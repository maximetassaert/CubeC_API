using System.ComponentModel.DataAnnotations;

namespace Cube_C___API.Models;

public class Customer
{
    public int Id { get; init; }
    // [Required]
    public string FirstName { get; set; }
    // [Required]
    public string LastName { get; set; }
    
    public DateTime Birthdate { get; set; }
    // [Required]
    public string MobileNumber { get; set; }

    public User User { get; set; }
    // [Required]
    public int UserId { get; set;  }

    // public Customer(string firstName, string lastName, DateTime birthdate, string mobileNumber, List<User> users)
    // {
    //     FirstName = firstName;
    //     LastName = lastName;
    //     Birthdate = birthdate;
    //     MobileNumber = mobileNumber;
    //     Users = users;
    // }
    
}
