using System.ComponentModel.DataAnnotations;

public class OrderCreateDto
{
    [Required] public int CustomerId { get; set; }

    [Required] public int CartId { get; set; } = new();
}