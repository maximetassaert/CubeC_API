namespace Cube_C___API.Models;

public class User
{
    public int Id { get; init; }
    
    public string Mail { get; set; }
    public string Password { get; set; }
    public ICollection<Roles> Roles { get; set; } = new List<Roles>();
}
