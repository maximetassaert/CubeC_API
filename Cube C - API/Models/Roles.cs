using System.ComponentModel.DataAnnotations;

namespace Cube_C___API.Models;

public class Roles
{
    public int Id { get; init; }
    [Required]
    public string Name { get; set; }
}