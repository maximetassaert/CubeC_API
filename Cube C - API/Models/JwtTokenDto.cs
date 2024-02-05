namespace Cube_C___API.Models;

public class JwtTokenDto
{
    public JwtTokenDto(string token, List<Role> roles)
    {
        BearerToken = token;
        List<string> rolesStr = new List<string>();
        roles.ForEach(role => rolesStr.Add(role.Name));
        Roles = rolesStr;
    }

    public string BearerToken { get; init; }
    public List<string> Roles { get; init; }


}