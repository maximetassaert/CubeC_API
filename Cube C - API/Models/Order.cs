namespace Cube_C___API.Models;

public class Order
{
    public int Id { get; init; }
    
    public DateTime CreateDate { get; init; }
    public Customer Customer { get; set; }
    public Cart Cart { get; set; }
}