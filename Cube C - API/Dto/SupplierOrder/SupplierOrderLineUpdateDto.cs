using System.ComponentModel.DataAnnotations;

namespace Cube_C___API.Dto.SupplierOrder;

public class SupplierOrderLineUpdateDto
{
    [Required] public int ProductId { get; set; }
    [Required] public int Quantity { get; set; }
}