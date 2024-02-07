using Cube_C___API.DTO.Category;
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

    private readonly IRepositoryData<Customer> _repositoryData;
    private readonly IRepositoryData<User> _repository;


    public CustomerController(IRepositoryData<Customer> repositoryData)
    {
        _repositoryData = repositoryData;
    }
    
    
    [HttpPost]
    public IActionResult PostCustomer(CustomerDto dto)
    {
        Customer customer = new Customer
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Birthdate = dto.Birthdate,
            MobileNumber = dto.MobileNumber,
        };

        foreach (int userId in dto.Users)
        {
            var user = _repository.GetById(userId);
            if (user != null)
            {
                customer.User = user ;
            }
        }

        _repositoryData.Create(customer);
        return CreatedAtAction(nameof(PostCustomer), new
        {
            Message = "Client créé",
            Customer = customer
        });
    }

    [HttpGet]
    [Authorize(Roles = Role.ADMIN)]
    public IActionResult GetCategories() 
    {
        return Ok(_repositoryData.GetAll());
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id) 
    {
        Customer customer = _repositoryData.GetById(id);
        if(customer != null)
        {
            return Ok(customer);
        }
        return NotFound(
            new{
                Message = "Client non trouvé"
            }
        );
    }


    [HttpPut("{id}")]
    public IActionResult UpdateCustomer(CustomerDto customersDto,int id)
    {
        var found = _repositoryData.GetById(id);
        if (found == null) return NotFound("Client introuvable");
        foreach (var prop in typeof(CustomerUpdateDto).GetProperties())
        {
            if(prop.GetValue(customersDto) != null)
            {
                found.GetType().GetProperty(prop.Name).SetValue(found, prop.GetValue(customersDto));
            }
                
        }

        _repositoryData.Update(found);
        return Ok(new
        {
            Message = "Client mis a jour",
            Category = found
        });

    }


    
    
    [HttpDelete("{id}")]
    public IActionResult DeleteCustomer(int id)
    {
        var found = _repositoryData.GetById(id);
        if (found == null) return NotFound("Client introuvable");
        _repositoryData.Delete(found);
        return Ok("Client suprimée");
    }
}
    
    
