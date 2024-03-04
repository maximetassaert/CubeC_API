using System.Security.Claims;
using Cube_C___API.Models;
using Cube_C___API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cube_C___API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly CartRepository _cartRepository;

    private readonly ILogger<CartController> _logger;
    private readonly OrderRepository _orderRepository;
    private readonly IRepositoryData<Product> _productRepository;
    private readonly SupplierOrderRepository _supplierOrderRepository;

    public OrderController(CartRepository cartRepository, OrderRepository orderRepository,
        IRepositoryData<Product> productRepository, SupplierOrderRepository supplierOrderRepository,
        ILogger<CartController> logger)
    {
        _cartRepository = cartRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _supplierOrderRepository = supplierOrderRepository;
        _logger = logger;
    }

    [HttpPost]
    public Order Create(OrderCreateDto orderDto)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity == null) throw new Exception("L'utilisateur n'est pas connecté ??");

        if (!Utils.IsAdminUser(identity) || orderDto.CustomerId == 0)
        {
            var customerId =
                int.Parse((identity.FindFirst("customerId") ?? throw new Exception("customerId inconnu ??")).Value);
            orderDto.CustomerId = customerId;
        }

        var cart = _cartRepository.GetById(orderDto.CartId);
        cart.Editable = false;

        var order = new Order();

        _cartRepository.UpdateAll(cart);
        order.CustomerId = orderDto.CustomerId;
        order.CartId = cart.Id;

        foreach (var cartLine in cart.CartLines)
        {
            cartLine.Product.StockValue -= cartLine.Quantity;
            _productRepository.Update(cartLine.Product);
            if (cartLine.Product.StockValue < 0)
            {
                var qtyToOrder = Math.Min(-cartLine.Product.StockValue, cartLine.Quantity);
                var supplierOrder = new SupplierOrder();
                var supplierOrderLine = new SupplierOrderLine();
                supplierOrderLine.Product = cartLine.Product;
                supplierOrderLine.Quantity = qtyToOrder;

                supplierOrder.SupplierOrderLines = new List<SupplierOrderLine>();
                supplierOrder.SupplierOrderLines.Add(supplierOrderLine);
                supplierOrder.Supplier = cartLine.Product.Supplier;

                _supplierOrderRepository.Create(supplierOrder);
            }
        }

        _orderRepository.Create(order);

        return order;
    }

    [HttpGet]
    [Authorize(Roles = Role.ADMIN)]
    public IEnumerable<Order> findAllOrders()
    {
        return _orderRepository.GetAll();
    }
}