using Cube_C___API.Models;
using Cube_C___API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cube_C___API.Controllers;

[ApiController]
[Route("[controller]")]

public class RolesController : ControllerBase
{
    
    private readonly ILogger<RolesController> _logger;
    private readonly RolesRepository RolesRepository;

    
    public RolesController(RolesRepository rolesRepository, ILogger<RolesController> logger)
    {
        RolesRepository = rolesRepository;
        _logger = logger;
    }
    

    [HttpGet]
    // [Route("FindAll")]
    public IEnumerable<Role> FindAllRoles()
    {
        return RolesRepository.FindAll();
    }

    [HttpPost]
    public void Create(Role role)
    {
        RolesRepository.Insert(role);
        RolesRepository.Save();
    }

    [HttpGet]
    [Route("{id}")]
    public Role FindById(int id)
    {
        return RolesRepository.FindById(id);
    }
}