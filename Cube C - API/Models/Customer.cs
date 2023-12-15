using System.ComponentModel.DataAnnotations;

namespace Cube_C___API.Models;

public class Customer
{
    public int Id { get; init; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    
    public DateTime Birthdate { get; set; }
    [Required]
    public string MobileNumber { get; set; }
    
    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }
    
    [Required]
    public int AddressId { get; set; }
    public Address? Address { get; set; }
}