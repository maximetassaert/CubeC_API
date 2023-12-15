using Cube_C___API.Repositories;
using Cube_C___API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cube_C___API.Controllers;

[ApiController]
[Route("[controller]")]
public class CartsController : ControllerBase
{
    private readonly ILogger<CartsController> _logger;
    private readonly CartsRepository CartsRepository;

    public CartsController(CartsRepository cartsRepository, ILogger<CartsController> logger)
    {
        CartsRepository = cartsRepository;
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Cart> FindAllCarts()
    {
        return CartsRepository.FindAll();
    }

    [HttpPost]
    public void Create(Cart cart)
    {
        CartsRepository.Insert(cart);
        CartsRepository.Save();
    }

    [HttpGet]
    [Route("{id}")]
    public Cart FindById(int id)
    {
        return CartsRepository.FindById(id);
    }
}