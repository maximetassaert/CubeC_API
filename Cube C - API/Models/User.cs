using System.ComponentModel.DataAnnotations;

namespace Cube_C___API.Models;

public class User
{
    public int Id { get; init; }
    
    [Required]
    public string Mail { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    public ICollection<Roles> Roles { get; set; } = new List<Roles>();
}
