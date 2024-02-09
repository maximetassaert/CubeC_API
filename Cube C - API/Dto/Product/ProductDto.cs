namespace Cube_C___API.Dto.Product;

public class ProductDto
{
    public int Id { get; init; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    public int StockValue { get; set; }

    public string Color { get; set; }

    public string Family { get; set; }

    public int SupplierId { get; set; }

    public List<int> Categories { get; set; } = new();
}