using Cube_C___API.Models;
using Cube_C___API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Role = Cube_C___API.Models.Role;

namespace Cube_C___API.Controllers;


[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{

    private readonly ILogger<CustomersController> _logger;
    private readonly CustomersRepository CustomersRepository;


    public CustomersController(CustomersRepository customersRepository, ILogger<CustomersController> logger)
    {
        CustomersRepository = customersRepository;
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Roles = Role.ADMIN)]
    public IEnumerable<Customer> FindAll()
    {
        return CustomersRepository.FindAll();
    }
    
    [HttpPost]
    public void Create(Customer customer)
    {
        CustomersRepository.Insert(customer);
        CustomersRepository.Save();
    }
}
    
    
