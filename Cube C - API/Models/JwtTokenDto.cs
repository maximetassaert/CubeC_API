namespace Cube_C___API.Models;

public class JwtTokenDto
{
    public JwtTokenDto(string token)
    {
        BearerToken = token;
    }

    public string BearerToken { get; init; }

}