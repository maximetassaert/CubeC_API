using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cube_C___API.Models
{
    public class User
    {
    public int Id { get; init; }
    
    [Required]
    public string Mail { get; set; }
    
    [Required]
    public string Password { get; set; }

    public List<Role> Roles { get; set; } = new List<Role>();
    }
    
}
