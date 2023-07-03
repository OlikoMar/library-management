using LMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace LMS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly LMSDbContext _context;

    public AuthorsController(LMSDbContext context)
    {
        _context = context;
    }

    // GET: api/Authors
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
    {
        if (!_context.Set<Author>().Any())
        {
            return NotFound();
        }

        return await _context.Set<Author>().ToListAsync();
    }

    // GET: api/Authors/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> GetAuthor(int id)
    {
        var author = await _context.Author.FindAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        return author;
    }

    // PUT: api/Authors/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAuthor(int id, Author author)
    {
        if (id != author.Id)
        {
            return BadRequest();
        }

        _context.Entry(author).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AuthorExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Authors
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Author>> PostAuthor(Author author)
    {
        _context.Author.Add(author);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
    }

    // DELETE: api/Authors/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var author = await _context.Author.FindAsync(id);
        if (author == null)
        {
            return NotFound();
        }

        _context.Author.Remove(author);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AuthorExists(int id)
    {
        return _context.Set<Author>().Any(e => e.Id == id);
    }
}