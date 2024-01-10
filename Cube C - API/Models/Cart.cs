using System.ComponentModel.DataAnnotations;

namespace Cube_C___API.Models;

public class Cart
{
    public int Id { get; init; }
    [Required] 
    public DateTime CreateDate { get; init; } = new DateTime();
    [Required]
    public int CustomerId { get; set; }

    public Customer? Customer { get; set; }

    public List<CartLine> CartLines { get; set; } = new List<CartLine>();

    public bool Editable { get; set; } = true;
}