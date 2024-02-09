using System.ComponentModel.DataAnnotations;

namespace Cube_C___API.Models;

public class SupplierOrder
{
    public bool Delivered { get; set; } = false;
    public int Id { get; init; }

    public DateTime CreateDate { get; init; } = DateTime.Now;

    [Required] public Supplier Supplier { get; set; }

    public int SupplierId { get; set; }
    [Required] public List<SupplierOrderLine> SupplierOrderLines { get; set; } = new();
}