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
    private readonly IRepositoryData<Product> _repositoryData;
    private readonly IRepositoryData<Category> _repositoryCategory;


    public ProductController(IRepositoryData<Product> repositoryData)
    {
        _repositoryData = repositoryData;
    }

    
    [HttpPost]
    public IActionResult PostProduct(ProductDto dto)
    {
        Product product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Image = dto.Image,
            StockValue = dto.StockValue,
            Color = dto.Color,
            Family = dto.Family,
        };

        foreach (int categoryId in dto.Categories)
        {
            var category = _repositoryCategory.GetById(categoryId);
            if (category != null)
            {
                product.Categories.Add(category);
            }
        }

        _repositoryData.Create(product);
        return CreatedAtAction(nameof(PostProduct), new
        {
            Message = "Produit créé",
            Product = product
        });
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetProducts() 
    {
        return Ok(_repositoryData.GetAll());
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id) 
    {
        Product product = _repositoryData.GetById(id);
        if(product != null)
        {
            return Ok(product);
        }
        return NotFound(
            new{
                Message = "Produit non trouvé"
            }
        );
    }



    [HttpPut("{id}")]
    public IActionResult UpdateProduct(ProductDto productDto,int id)
    {
        var found = _repositoryData.GetById(id);
        if (found == null) return NotFound("Client introuvable");
        foreach (var prop in typeof(ProductUpdateDto).GetProperties())
        {
            if(prop.GetValue(productDto) != null)
            {
                found.GetType().GetProperty(prop.Name).SetValue(found, prop.GetValue(productDto));
            }
                
        }

        _repositoryData.Update(found);
        return Ok(new
        {
            Message = "Produit mis a jour",
            Category = found
        });

    }
    
    
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var found = _repositoryData.GetById(id);
        if (found == null) return NotFound("Produit introuvable");
        _repositoryData.Delete(found);
        return Ok("Produit suprimée");
    }


}