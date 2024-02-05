using Cube_C___API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cube_C___API.Controllers;


[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{

    private readonly ILogger<UsersController> _logger;
    private readonly JwtAuthenticationService _jwtAuthenticationService;



    public AuthController(JwtAuthenticationService jwtAuthenticationService, ILogger<UsersController> logger)
    {
        _jwtAuthenticationService = jwtAuthenticationService;
        _logger = logger;
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous] 
    public IActionResult Post([FromBody] LoginDto model)
    {
        User? user = _jwtAuthenticationService.Authenticate(model.Email, model.Password);

        if (user == null)
        {
            return Unauthorized();
        }
        
        HttpContext.Session.SetString("coucou", "salut bg blablablabalablabaalal");

        string token = _jwtAuthenticationService.GenerateToken(user);

        return new JsonResult(new JwtTokenDto(token, user.Roles));

    }

    
}
    
    
