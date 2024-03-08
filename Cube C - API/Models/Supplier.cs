using System.ComponentModel.DataAnnotations;

namespace Cube_C___API.Models;

public class Supplier
{
    public int Id { get; init; }

    [Required] public string CompanyName { get; set; }

    public string VatNumber { get; set; }

    [Required] public string MobileNumber { get; set; }

    public int? UserId { get; set; }

    public User? User { get; set; }

    public int? AddressId { get; set; }

    public Address? Address { get; set; }
}