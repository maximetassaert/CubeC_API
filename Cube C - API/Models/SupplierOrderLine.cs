using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cube_C___API.Models;

public class SupplierOrderLine
{
    public int Id { get; init; }

    [Required] [JsonIgnore] public int SupplierOrderId { get; set; }

    [JsonIgnore] public SupplierOrder? SupplierOrder { get; set; }

    [Required] public int ProductId { get; set; }

    public Product? Product { get; set; }

    [Required] public int Quantity { get; set; }
}