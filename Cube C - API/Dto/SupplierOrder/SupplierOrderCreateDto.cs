using System.ComponentModel.DataAnnotations;

namespace Cube_C___API.Dto.SupplierOrder;

public class SupplierOrderCreateDto
{
    public int SupplierId { get; set; }

    [Required] public List<SupplierOrderLineCreateDto> SupplierOrderLinesCreateDto { get; set; } = new();
}