using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Data;
using Identity.Infrastructure;
using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _dbContext;
        public UserController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            AppDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _dbContext = dbContext;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegistrationModel model)
        {
            if (await _userManager.Users.AnyAsync(s =>
                    s.NormalizedEmail == model.Email.ToUpper()))
            {
                var problemDetails = new ValidationProblemDetails();
                problemDetails.Errors.Add(nameof(model.Email), new[] { "User exists" });
                return new BadRequestObjectResult(problemDetails);
            }
            var user = new IdentityUser(Guid.NewGuid().ToString()) { Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var problemDetails = new ValidationProblemDetails();
                problemDetails.Errors.Add(nameof(model.Password), result.Errors.Select(x => x.Description).ToArray());
                return new BadRequestObjectResult(problemDetails);
            }
            return Ok();
        }
        
        [HttpPost("auth")]
        public async Task<IActionResult> AuthAsync(AuthModel model)
        {
            var user = await _signInManager.UserManager.Users.FirstOrDefaultAsync(s =>
                 s.NormalizedEmail == model.Email.ToUpper());
            if (user is null)
            {
                var problemDetails = new ValidationProblemDetails();
                problemDetails.Errors.Add(nameof(model.Email), new[] { "User not exists" });
                return new BadRequestObjectResult(problemDetails);
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);
            if (!result.Succeeded)
            {
                var problemDetails = new ValidationProblemDetails();
                problemDetails.Errors.Add(nameof(model.Email), new[] { "User not exists" });
                return new BadRequestObjectResult(problemDetails);
            }
           
            return Ok(await GenerateJwtTokenAsync(user));
        }
        
        [Authorize]
        [HttpGet("check-auth")]
        public IActionResult CheckAuth()
        {
            return Ok();
        }
        
        private async Task<string> GenerateJwtTokenAsync(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var jwtHeader =
                new JwtHeader(new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
            var now = DateTime.UtcNow;
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.UserName)
            };
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            await _userManager.AddClaimsAsync(user, claims);
            await _userManager.UpdateAsync(user);
            var tokenDescriptor = new JwtPayload(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                null,
                now.AddHours(1)
                );
            var jwt = new JwtSecurityToken(jwtHeader, tokenDescriptor);
            return tokenHandler.WriteToken(jwt);
        }
    }
}