using System.ComponentModel.DataAnnotations;

namespace Identity.Models;

public class RegistrationModel
{
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required] public string Password { get; set; } = default!;
}