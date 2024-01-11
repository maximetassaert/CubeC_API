using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cube_C___API.Models;

public class Cart
{
    public int Id { get; init; }
    public DateTime CreateDate { get; init; } = DateTime.Now;
    [Required]
    public int CustomerId { get; set; }
    [JsonIgnore]
    public Customer? Customer { get; set; }

    public List<CartLine> CartLines { get; set; } = new List<CartLine>();

    public bool Editable { get; set; } = true;
}