using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cube_C___API.Models;

public class CartLine
{
    public int Id { get; init; }
    [Required]
    [JsonIgnore]
    public int CartId { get; set; }
    [JsonIgnore]
    public Cart? Cart { get; set; }
    
    [Required]
    public int ProductId { get; set; }
    [JsonIgnore]
    public Product? Product { get; set; }
    
    [Required]
    public int Quantity { get; set; }
}