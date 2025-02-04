using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cube_C___API.Models;

public class Role
{
    public const string ADMIN = "admin";
    
    public int Id { get; init; }
    [Required]
    public string Name { get; set; }
    [JsonIgnore]
    public List<User> Users { get; set; } = new List<User>();

    
}
