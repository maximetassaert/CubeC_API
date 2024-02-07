using Cube_C___API.Dtos.User;
using Cube_C___API.Models;
using Cube_C___API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cube_C___API.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly UserRepository _userRepository;


    public UserController(UserRepository userRepository, ILogger<UserController> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }
    
    [HttpPost]
    [AllowAnonymous] 
    public void Create(User user)
    {
        user.Password = Utils.HashPassword(user.Password);
        
        _userRepository.Create(user);

    }
    
    [HttpGet]
    [Authorize(Roles = Role.ADMIN)]
    public IEnumerable<GetUserDto> FindAllUsers()
    {
        return _userRepository.FindAll();
    }

    [HttpGet]
    [Route("{id}")]
    public GetUserDto FindById(int id)
    {
        return _userRepository.FindById(id);
    }

    [HttpPatch]
    public void Update(User user)
    {
        _userRepository.Update(user);
    }
        
}
    
    
