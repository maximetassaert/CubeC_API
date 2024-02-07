using System.Security.Claims;
using Cube_C___API.Dtos.Cart;
using Cube_C___API.Repositories;
using Cube_C___API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cube_C___API.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly ILogger<CartController> _logger;
    private readonly CartRepository _cartRepository;

    public CartController(CartRepository cartRepository, ILogger<CartController> logger)
    {
        _cartRepository = cartRepository;
        _logger = logger;
    }
    
    [HttpPost]
    public Cart Create(Cart cart)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity == null) throw new Exception("L'utilisateur n'est pas connecté ??");

        if (!Utils.IsAdminUser(identity) || cart.CustomerId == 0)
        {
            var customerId =
                int.Parse((identity.FindFirst("customerId") ?? throw new Exception("customerId inconnu ??")).Value);
            cart.CustomerId = customerId;
        }
        _cartRepository.Create(cart);
        return cart;
    }

    [HttpGet]
    public IEnumerable<Cart> FindAllCarts()
    {
        return _cartRepository.GetAll();
    }



    [HttpGet]
    [Route("{id}")]
    public Cart? FindById(int id)
    {
        if (id == 0)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null) throw new Exception("L'utilisateur n'est pas connecté ??");

        
            var customerId =
                int.Parse((identity.FindFirst("customerId") ?? throw new Exception("customerId inconnu ??")).Value);
        
            return _cartRepository.FindCartByCustomer(customerId);
        }
        return _cartRepository.FindCartByCustomer(id);
    }
    
    [HttpPut]
    public Cart Update(Cart cart)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity == null) throw new Exception("L'utilisateur n'est pas connecté ??");

        if (!Utils.IsAdminUser(identity) || cart.CustomerId == 0)
        {
            var customerId =
                int.Parse((identity.FindFirst("customerId") ?? throw new Exception("customerId inconnu ??")).Value);
            cart.CustomerId = customerId;
        }
        
        _cartRepository.UpdateAll(cart);
        return cart;
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteIngredient(int id)
    {
        var found = _cartRepository.GetById(id);
        if (found == null) return NotFound("Ingredient introuvable");
        _cartRepository.Delete(found);
        return Ok("Ingredient suprimé");
    }
}