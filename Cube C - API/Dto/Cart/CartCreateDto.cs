using System.ComponentModel.DataAnnotations;
using Cube_C___API.Models;

namespace Cube_C___API.Dto.Cart;

public class CartCreateDto
{
    [Required] public int Id { get; init; }

    [Required] public int CustomerId { get; set; }

    [Required] public List<CartLine> CartLines { get; set; } = new();
}