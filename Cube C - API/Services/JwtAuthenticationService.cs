using Cube_C___API.Models;
using Cube_C___API.Repositories;

namespace Cube_C___API;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;

public class JwtAuthenticationService
{

    private readonly UserRepository _userRepository;
    private readonly CustomerRepository _customerRepository;

    public JwtAuthenticationService(UserRepository userRepository, CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
        _userRepository = userRepository;
    }
    
    public User? Authenticate(string email, string password)
    {
        User? user = _userRepository.findByEmail(email);
        if (user == null)
        {
            // user existe pas
            return null;
        }
        
        if (!Utils.VerifyPassword(password, user.Password))
        {
            // mauvais password
            return null;
        }

        return user;
    }

    public string GenerateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Mail),
            new Claim("userId", user.Id.ToString())
        };

        var customer = _customerRepository.FindByUserId(user.Id);
        if (customer != null)
        {
            claims.Add(new Claim("customerId", customer.Id.ToString()));
        }

        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
        }
        
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Jwt-secret") ?? throw new Exception("Secret Jwt non définit !")));
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}