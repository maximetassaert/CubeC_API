using System.ComponentModel.DataAnnotations;

namespace Cube_C___API.Models;

public class Address
{
    public int Id { get; init; }
    [Required]
    public string AddressLine{ get; set; }
    [Required]
    public string PostCode { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string Country { get; set; }
}
