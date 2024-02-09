using System.ComponentModel.DataAnnotations;

namespace Cube_C___API.Dto.SupplierOrder;

public class SupplierOrderUpdateDto
{
    public int Id { get; set; }

    public bool Delivered { get; set; }
    [Required] public List<SupplierOrderLineUpdateDto> SupplierOrderLines { get; set; } = new();
}