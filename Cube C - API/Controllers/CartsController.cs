using System.Security.Claims;
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
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity == null) throw new Exception("L'utilisateur n'est pas connecté ??");

        if (!Utils.IsAdminUser(identity) && cart.Id == 0)
        {
            var customerId =
                int.Parse((identity.FindFirst("customerId") ?? throw new Exception("customerId inconnu ??")).Value);
            cart.CustomerId = customerId;
        }
        CartsRepository.Insert(cart);
        CartsRepository.Save();
    }

    [HttpGet]
    [Route("{id}")]
    public Cart FindById(int id)
    {
        if (id == 0)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null) throw new Exception("L'utilisateur n'est pas connecté ??");

        
            var customerId =
                int.Parse((identity.FindFirst("customerId") ?? throw new Exception("customerId inconnu ??")).Value);
        
            return CartsRepository.FindCartByCustomer(customerId);
        }
        return CartsRepository.FindById(id);
    }
}