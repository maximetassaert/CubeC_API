using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cube_C___API.Models;
using Cube_C___API.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace Cube_C___API;

public class JwtAuthenticationService
{
    private readonly CustomerRepository _customerRepository;

    private readonly UserRepository _userRepository;

    public JwtAuthenticationService(UserRepository userRepository, CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
        _userRepository = userRepository;
    }

    public User? Authenticate(string email, string password)
    {
        var user = _userRepository.findByEmail(email);
        if (user == null)
            // user existe pas
            return null;

        if (!Utils.VerifyPassword(password, user.Password))
            // mauvais password
            return null;

        return user;
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Mail),
            new("userId", user.Id.ToString())
        };

        var customer = _customerRepository.FindByUserId(user.Id);

        if (customer != null) claims.Add(new Claim("customerId", customer.Id.ToString()));

        foreach (var role in user.Roles) claims.Add(new Claim(ClaimTypes.Role, role.Name));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Jwt-secret") ??
                                                                  throw new Exception("Secret Jwt non définit !")));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}