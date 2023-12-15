namespace Cube_C___API.Models;

public class Customer
{
    public int Id { get; init; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateTime Birthdate { get; set; }
    
    public string Mail { get; set; }
    
    public string MobileNumber { get; set; }
    
    public User User { get; set; }
    
    public Address Address { get; set; }
}