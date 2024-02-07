
using Cube_C___API.Models;
namespace Cube_C___API.Repositories;

public class AddressRepository : BaseRepository, IRepositoryData<Address>
{
    private readonly ApplicationDbContext _dbcontext;

    public AddressRepository(ApplicationDbContext dbContext) : base(dbContext)
    {}
    
    public Address GetById(int id)
    {
        return _dbcontext.Addresses.Find(id);
    }

    public List<Address> GetAll()
    {
        return _dbcontext.Addresses.ToList();
    }

    public bool Create(Address entity)
    {
        _dbcontext.Add(entity);
        _dbcontext.SaveChanges();
        return true;
    }

    public bool Update(Address entity)
    {
        _dbcontext.Update(entity);
        _dbcontext.SaveChanges();
        return true;
    }

    public bool Delete(Address entity)
    {
        _dbcontext.Remove(entity);
        _dbcontext.SaveChanges();
        return true;
    }
    
}