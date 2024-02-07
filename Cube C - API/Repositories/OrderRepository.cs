using Cube_C___API.Models;

namespace Cube_C___API.Repositories;


public class OrderRepository : BaseRepository, IRepositoryData<Order>
{

    public OrderRepository(ApplicationDbContext dbContext): base(dbContext)
    {}


    public Order GetById(int id)
    {
        return _dbContext.Orders.Find(id);
    }

    public List<Order> GetAll()
    {
        return _dbContext.Orders.ToList();
    }

    public bool Create(Order entity)
    {
        _dbContext.Add(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Update(Order entity)
    {
        _dbContext.Update(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Delete(Order entity)
    {
        _dbContext.Remove(entity);
        _dbContext.SaveChanges();
        return true;
    }
}
