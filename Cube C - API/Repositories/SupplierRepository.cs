using Cube_C___API.Models;


namespace Cube_C___API.Repositories;


public class SupplierRepository : BaseRepository, IRepositoryData<Supplier>
{
    
    public SupplierRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }


    public Supplier GetById(int id)
    {
        return _dbContext.Suppliers.Find(id);
    }

    public List<Supplier> GetAll()
    {
        return _dbContext.Suppliers.ToList();
    }

    public bool Create(Supplier entity)
    {
        _dbContext.Add(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Update(Supplier entity)
    {
        _dbContext.Update(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Delete(Supplier entity)
    {
        _dbContext.Remove(entity);
        _dbContext.SaveChanges();
        return true;
    }
}


