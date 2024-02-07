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
    public float Price { get; set; }
    [Required]
    public string Color { get; set; }
    [Required]
    public string Family { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
    [JsonIgnore]
    public Category? Category { get; set; }
}