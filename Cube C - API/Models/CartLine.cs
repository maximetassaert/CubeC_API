using System.ComponentModel.DataAnnotations;

namespace Cube_C___API.Models;

public class CartLine
{
    public int Id { get; init; }
    [Required]
    public int CartId { get; set; }
    public Cart? Cart { get; set; }
    
    [Required]
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    
    [Required]
    public int Quantity { get; set; }
}