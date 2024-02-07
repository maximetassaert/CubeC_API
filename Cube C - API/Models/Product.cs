using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cube_C___API.Models;

public class Product
{
    public int Id { get; init; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Image { get; set; }
    [Required]
    public string StockValue { get; set; }
    [Required]
    public string Color { get; set; }
    [Required]
    public string Family { get; set; }

    public List<Category> Categories { get; set; } = new List<Category>();

    // public Product(string name, string description, string image, string stockValue, string color, string family,
    //     List<Category> categories)
    // {
    //     Name = name;
    //     Description = description;
    //     Image = image;
    //     StockValue = stockValue;
    //     Color = color;
    //     Family = family;
    //     Categories = categories;
    // }
}