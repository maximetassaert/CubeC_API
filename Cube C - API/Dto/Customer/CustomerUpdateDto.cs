namespace Cube_C___API.Dto.Customer;

public class CustomerUpdateDto
{
    public int? Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? Birthdate { get; set; }

    public string? MobileNumber { get; set; }
}