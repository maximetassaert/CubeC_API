using Cube_C___API.Models;
namespace Cube_C___API.Repositories;

public class CartLineRepository : BaseRepository, IRepositoryData<CartLine>
{
    
    public CartLineRepository(ApplicationDbContext dbContext): base(dbContext)
    {}


    public CartLine GetById(int id)
    {
        return _dbContext.CartLines.Find(id);
    }

    public List<CartLine> GetAll()
    {
        return _dbContext.CartLines.ToList();
    }

    public bool Create(CartLine entity)
    {
        _dbContext.Add(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Update(CartLine entity)
    {
        _dbContext.Update(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Delete(CartLine entity)
    {
        _dbContext.Remove(entity);
        _dbContext.SaveChanges();
        return true;
    }
}