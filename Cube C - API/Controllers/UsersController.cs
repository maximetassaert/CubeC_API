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
    //[Route("findAll")]
    public IEnumerable<User> FindAll()
    {
        return UsersRepository.GetStudents();
    }
    
    [HttpPost(Name = "create")]
    public void CreateUser(User user)
    {
        UsersRepository.InsertUser(user);
        UsersRepository.Save();
    }
}
    
    
