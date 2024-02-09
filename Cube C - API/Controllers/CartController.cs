using System.Security.Claims;
using Cube_C___API.Dto.Cart;
using Cube_C___API.Models;
using Cube_C___API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cube_C___API.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly CartRepository _cartRepository;
    private readonly ILogger<CartController> _logger;

    public CartController(CartRepository cartRepository, ILogger<CartController> logger)
    {
        _cartRepository = cartRepository;
        _logger = logger;
    }

    [HttpPost]
    public Cart Create(CartCreateDto cartDto)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity == null) throw new Exception("L'utilisateur n'est pas connecté ??");

        if (!Utils.IsAdminUser(identity) || cartDto.CustomerId == 0)
        {
            var customerId =
                int.Parse((identity.FindFirst("customerId") ?? throw new Exception("customerId inconnu ??")).Value);
            cartDto.CustomerId = customerId;
        }

        Cart cart = new();
        cart.CartLines = cartDto.CartLines;
        cart.CustomerId = cartDto.CustomerId;


        _cartRepository.Create(cart);
        return cart;
    }

    [HttpGet]
    [Authorize(Roles = Role.ADMIN)]
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
    [Authorize(Roles = Role.ADMIN)]
    public IActionResult DeleteCart(int id)
    {
        var found = _cartRepository.GetById(id);
        if (found == null) return NotFound("Panier introuvable");
        _cartRepository.Delete(found);
        return Ok("Panier suprimé");
    }
}