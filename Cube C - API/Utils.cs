using System.Security.Claims;
using System.Security.Principal;
using Cube_C___API.Models;

namespace Cube_C___API;

public class Utils
{
    public static bool IsAdminUser(IIdentity identity)
    {
        var claimsIdentity = identity as ClaimsIdentity;
        
        if (claimsIdentity == null) throw new Exception("L'utilisateur n'est pas connecté ??");

        var roleClaims = claimsIdentity.FindAll(claim => claim.Type == ClaimTypes.Role);
        
        foreach (var roleClaim in roleClaims)
        {
            if (roleClaim.Value == Role.ADMIN)
            {
                return true;
            }
        }

        return false;
    }
    
    public static string HashPassword(string plainPassword)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
		
        return BCrypt.Net.BCrypt.HashPassword(plainPassword, salt);
    }
	
    public static bool VerifyPassword(string plainPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
    }
}