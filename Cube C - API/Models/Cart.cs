namespace Cube_C___API.Models;

public class Cart
{
    public int Id { get; init; }
    
    public DateTime CreateDate { get; init; }
    public Customer Customer { get; init; }
}