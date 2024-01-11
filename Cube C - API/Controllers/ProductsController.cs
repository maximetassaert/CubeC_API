using Cube_C___API.Models;
using Cube_C___API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cube_C___API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly ProductsRepository ProductsRepository;

    public ProductsController(ProductsRepository productsRepository, ILogger<ProductsController> logger)
    {
        ProductsRepository = productsRepository;
        _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public IEnumerable<Product> FindAllProducts()
    {
        return ProductsRepository.FindAll();
    }

    [HttpPost]
    public void Create(Product product)
    {
        ProductsRepository.Insert(product);
        ProductsRepository.Save();
    }

    [HttpGet]
    [Route("{id}")]
    public Product FindById(int id)
    {
        return ProductsRepository.FindById(id);
    }
}