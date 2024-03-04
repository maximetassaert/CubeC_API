using System.ComponentModel.DataAnnotations;

namespace Cube_C___API.Models;

public class Order
{
    public int Id { get; init; }

    public DateTime CreateDate { get; init; } = DateTime.Now;

    [Required] public Customer Customer { get; set; }

    public int CustomerId { get; set; }

    [Required] public int CartId { get; set; }

    public Cart? Cart { get; set; }
}