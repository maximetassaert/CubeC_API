using Cube_C___API.Dto.Product;
using Cube_C___API.Models;
using Cube_C___API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cube_C___API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IRepositoryData<Category> _repositoryCategory;
    private readonly IRepositoryData<Product> _repositoryProduct;
    private readonly IRepositoryData<Supplier> _repositorySupplier;


    public ProductController(IRepositoryData<Product> repositoryProduct, IRepositoryData<Category> repositoryCategory,
        IRepositoryData<Supplier> repositorySupplier)
    {
        _repositoryProduct = repositoryProduct;
        _repositoryCategory = repositoryCategory;
        _repositorySupplier = repositorySupplier;
    }


    [HttpPost]
    [Authorize(Roles = Role.ADMIN)]
    public IActionResult PostProduct(ProductDto dto)
    {
        var supplier = _repositorySupplier.GetById(dto.SupplierId);
        if (supplier == null) return BadRequest("Le fournisseur n'existe pas");

        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Image = dto.Image,
            StockValue = dto.StockValue,
            Color = dto.Color,
            Family = dto.Family,
            Supplier = supplier
        };

        foreach (var categoryId in dto.Categories)
        {
            var category = _repositoryCategory.GetById(categoryId);
            if (category != null) product.Categories.Add(category);
        }

        _repositoryProduct.Create(product);
        return CreatedAtAction(nameof(PostProduct), product);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetProducts()
    {
        return Ok(_repositoryProduct.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = _repositoryProduct.GetById(id);
        if (product != null) return Ok(product);
        return NotFound(
            new
            {
                Message = "Produit non trouvé"
            }
        );
    }


    [HttpPut("{id}")]
    [Authorize(Roles = Role.ADMIN)]
    public IActionResult UpdateProduct(ProductUpdateDto productUpdateDto, int id)
    {
        var found = _repositoryProduct.GetById(id);
        if (found == null) return NotFound("Produit introuvable");
        foreach (var prop in typeof(ProductUpdateDto).GetProperties())
            if (prop.GetValue(productUpdateDto) != null)
                found.GetType().GetProperty(prop.Name).SetValue(found, prop.GetValue(productUpdateDto));

        _repositoryProduct.Update(found);
        return Ok(found);
    }


    [HttpDelete("{id}")]
    [Authorize(Roles = Role.ADMIN)]
    public IActionResult DeleteProduct(int id)
    {
        var found = _repositoryProduct.GetById(id);
        if (found == null) return NotFound("Produit introuvable");
        _repositoryProduct.Delete(found);
        return Ok("Produit suprimée");
    }
}