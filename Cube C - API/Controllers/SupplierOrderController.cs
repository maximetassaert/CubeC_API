using Cube_C___API.Dto.SupplierOrder;
using Cube_C___API.Models;
using Cube_C___API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cube_C___API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = Role.ADMIN)]
public class SupplierOrderController : ControllerBase
{
    private readonly IRepositoryData<Product> _productRepository;
    private readonly SupplierOrderRepository _supplierOrderRepository;

    public SupplierOrderController(SupplierOrderRepository supplierOrderRepository,
        IRepositoryData<Product> repositoryProduct)
    {
        _supplierOrderRepository = supplierOrderRepository;
        _productRepository = repositoryProduct;
    }

    [HttpPost]
    public SupplierOrder Create(SupplierOrderCreateDto supplierOrderDto)
    {
        SupplierOrder supplierOrder = new();
        supplierOrder.SupplierId = supplierOrderDto.SupplierId;
        foreach (var supplierOrderLineCreateDto in supplierOrderDto.SupplierOrderLinesCreateDto)
        {
            SupplierOrderLine supplierOrderLine = new();
            supplierOrderLine.ProductId = supplierOrderLineCreateDto.ProductId;
            supplierOrderLine.Quantity = supplierOrderLineCreateDto.Quantity;

            supplierOrder.SupplierOrderLines.Add(supplierOrderLine);
        }

        _supplierOrderRepository.Create(supplierOrder);
        return _supplierOrderRepository.GetById(supplierOrder.Id);;
    }

    [HttpGet]
    public IEnumerable<SupplierOrder> FindAllSupplierOrders()
    {
        return _supplierOrderRepository.GetAll();
    }

    [HttpPut("{id}")]
    public IActionResult Update(SupplierOrderUpdateDto supplierOrderDto, int id)
    {
        foreach (var supplierOrderLine in supplierOrderDto.SupplierOrderLines)
        {
            var product = _productRepository.GetById(supplierOrderLine.ProductId);
            product.StockValue += supplierOrderLine.Quantity; //StockValue string au lieu d'int + la commande se mets pas en livrée
            _productRepository.Update(product);
        }

        var found = _supplierOrderRepository.GetById(id);
        if (found == null) return NotFound("Commande fournisseur introuvable");

        found.Delivered = supplierOrderDto.Delivered;
        _supplierOrderRepository.Update(found);
        return Ok("c'est fait !");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSupplierOrder(int id)
    {
        var found = _supplierOrderRepository.GetById(id);
        if (found == null) return NotFound("Commande Fournisseur introuvable");
        _supplierOrderRepository.Delete(found);
        return Ok("Commande Fournisseur suprimé");
    }
}