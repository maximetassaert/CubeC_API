namespace Cube_C___API.Models;

public class CartLine
{
    public int Id { get; init; }
    public Cart Cart { get; set; }
    
    public Product Product { get; set; }
    public int Quantity { get; set; }
}