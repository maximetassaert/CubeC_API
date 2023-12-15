namespace Cube_C___API.Models;

public class Product
{
    public int Id { get; init; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string Image { get; set; }
    
    public string StockValue { get; set; }
    
    public string Color { get; set; }
    
    public string Family { get; set; }
    
    public Category Category { get; set; }
}