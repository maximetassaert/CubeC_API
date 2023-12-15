namespace Cube_C___API.Models;

public class Supplier
{
    public int Id { get; init; }
    
    public string CompanyName { get; set; }
    
    public string VatNumber { get; set; }
    
    public string MobileNumber { get; set; }
    
    public string Mail { get; set; }
    
    public User User { get; set; }
    
    public Address Address { get; set; }
}