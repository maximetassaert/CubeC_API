using Cube_C___API.Models;

namespace Cube_C___API.Dto.Product;

public class ProductUpdateDto
{
    public int Id { get; init; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    public string StockValue { get; set; }

    public string Color { get; set; }

    public string Family { get; set; }

    public float Price { get; set; }


    public List<Category> Categories { get; set; } = new();
}