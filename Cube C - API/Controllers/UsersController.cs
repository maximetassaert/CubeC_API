using Cube_C___API.Models;
using Cube_C___API.Repositories;
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
    // [Route("FindAll")]
    public IEnumerable<User> FindAllUsers()
    {
        return UsersRepository.FindAll();
    }
    
    [HttpPost]
    public void Create(User user)
    {
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
    
    
