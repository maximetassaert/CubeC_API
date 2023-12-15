using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cube_C___API.Models;

public class Roles
{
    public int Id { get; init; }
    [Required]
    public string Name { get; set; }
    [JsonIgnore]
    public List<User> Users { get; set; } = new List<User>();

}