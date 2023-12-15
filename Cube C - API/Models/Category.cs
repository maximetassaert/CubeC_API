using System.ComponentModel.DataAnnotations;

namespace Cube_C___API.Models;

public class Category
{
    public int Id { get; init; }
    [Required]
    public string Name { get; set; }

    public Category(string name)
    {
        Name = name;
    }
}

public class CategoryDto
{
    public string Name;
}