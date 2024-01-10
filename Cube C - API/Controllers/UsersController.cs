using Cube_C___API.Models;
using Cube_C___API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cube_C___API.Controllers;


[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{

    private readonly ILogger<UsersController> _logger;
    private readonly UsersRepository UsersRepository;


    public UsersController(UsersRepository usersRepository, ILogger<UsersController> logger)
    {
        UsersRepository = usersRepository;
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Roles = Role.ADMIN)]
    public IEnumerable<User> FindAllUsers()
    {
        return UsersRepository.FindAll();
    }
    
    [HttpPost]
    [AllowAnonymous] 
    public void Create(User user)
    {
        user.Password = Utils.HashPassword(user.Password);
        
        UsersRepository.Insert(user);
        UsersRepository.Save();
    }

    [HttpGet]
    [Route("{id}")]
    public User FindById(int id)
    {
        return UsersRepository.FindById(id);
    }
        
}
    
    
