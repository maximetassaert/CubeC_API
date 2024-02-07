using Cube_C___API.Dto.Customer;
using Cube_C___API.Models;
using Cube_C___API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Role = Cube_C___API.Models.Role;

namespace Cube_C___API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IRepositoryData<Customer> _customerRepository;
    private readonly IRepositoryData<User> _userRepository;


    public CustomerController(IRepositoryData<Customer> customerRepository, IRepositoryData<User> userRepository)
    {
        _customerRepository = customerRepository;
        _userRepository = userRepository;

    }


    [HttpPost]
    public IActionResult PostCustomer(CustomerDto dto)
    {
        var user = _userRepository.GetById(dto.UserId);

        var customer = new Customer
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Birthdate = dto.Birthdate,
            MobileNumber = dto.MobileNumber,
            User = user
        };


        _customerRepository.Create(customer);
        return Ok(customer);
    }

    [HttpGet]
    [Authorize(Roles = Role.ADMIN)]
    public IActionResult GetCategories()
    {
        return Ok(_customerRepository.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var customer = _customerRepository.GetById(id);

        if (customer != null) return Ok(customer);
        return NotFound(
            new
            {
                Message = "Client non trouvé"
            }
        );
    }


    [HttpPut("{id}")]
    public IActionResult UpdateCustomer(CustomerDto customersDto, int id)
    {
        var found = _customerRepository.GetById(id);
        if (found == null) return NotFound("Client introuvable");
        foreach (var prop in typeof(CustomerUpdateDto).GetProperties())
            if (prop.GetValue(customersDto) != null)
                found.GetType().GetProperty(prop.Name).SetValue(found, prop.GetValue(customersDto));

        _customerRepository.Update(found);
        return Ok(new
        {
            Message = "Client mis a jour",
            Category = found
        });
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteCustomer(int id)
    {
        var found = _customerRepository.GetById(id);
        if (found == null) return NotFound("Client introuvable");
        _customerRepository.Delete(found);
        return Ok("Client suprimée");
    }
}