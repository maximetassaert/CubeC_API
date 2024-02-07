using Cube_C___API.Models;
namespace Cube_C___API.Repositories;

public class ProductRepository : BaseRepository, IRepositoryData<Product>
{

    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext) {}


    public Product GetById(int id)
    {
        return _dbContext.Products.Find(id);
    }

    public List<Product> GetAll()
    {
        return _dbContext.Products.ToList();
    }

    public bool Create(Product entity)
    {
        _dbContext.Add(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Update(Product entity)
    {
        _dbContext.Update(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Delete(Product entity)
    {
        _dbContext.Remove(entity);
        _dbContext.SaveChanges();
        return true;
    }
}