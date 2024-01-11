using System.ComponentModel.DataAnnotations;

namespace Cube_C___API.Models;

public class LoginDto
{
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}