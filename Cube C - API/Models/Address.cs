namespace Cube_C___API.Models;

public class Address
{
    public int Id { get; init; }
    
    public string AddressLine{ get; set; }
    
    public string PostCode { get; set; }
    
    public string City { get; set; }
    
    public string Country { get; set; }
}