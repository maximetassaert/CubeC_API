using System.Text.Json.Serialization;

namespace Cube_C___API.Models;

public class Category
{
    [JsonIgnore] public List<Product> Products { get; set; } = new();

    public int Id { get; init; }
    public string Name { get; set; }
}