using Cube_C___API.Dto.Supplier;
using Cube_C___API.Models;
using Cube_C___API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cube_C___API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = Role.ADMIN)]
public class SupplierController : ControllerBase
{
    private readonly IRepositoryData<Supplier> _repositoryData;


    public SupplierController(IRepositoryData<Supplier> repositoryData)
    {
        _repositoryData = repositoryData;
    }

    [HttpPost]
    public IActionResult CreateSupplier(SupplierDto supplierDto)
    {
        Supplier supplier = new();
        supplier.CompanyName = supplierDto.CompanyName;
        supplier.VatNumber = supplierDto.VatNumber;
        supplier.MobileNumber = supplierDto.MobileNumber;
        if (_repositoryData.Create(supplier)) return Ok(supplier);
        return BadRequest();
    }

    [HttpGet]
    // [Route("FindAll")]
    public IActionResult GetAllSuppliers()
    {
        return Ok(_repositoryData.GetAll());
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetSupplierById(int id)
    {
        var supplier = _repositoryData.GetById(id);
        if (supplier != null) return Ok(supplier);

        return NotFound(
            new
            {
                Message = "Fournisseur non trouvé"
            });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSupplier(SupplierUpdateDto supplierUpdateDto, int id)
    {
        var found = _repositoryData.GetById(id);
        if (found == null) return NotFound("Role introuvable");
        foreach (var prop in typeof(SupplierUpdateDto).GetProperties())
            if (prop.GetValue(supplierUpdateDto) != null)
                found.GetType().GetProperty(prop.Name).SetValue(found, prop.GetValue(supplierUpdateDto));

        _repositoryData.Update(found);
        return Ok(new
        {
            Message = "Fournisseur mis a jour",
            Role = found
        });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSupplier(int id)
    {
        var found = _repositoryData.GetById(id);
        if (found == null) return NotFound("Fournisseur introuvable");
        _repositoryData.Delete(found);
        return Ok("Fournisseur supprimé");
    }
}