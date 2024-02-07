using Cube_C___API.Dtos.Role;
using Cube_C___API.Models;

namespace Cube_C___API.Repositories;

public class RoleRepository : BaseRepository, IRepositoryData<Role>
{
    public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {}
    
    public Role GetById(int id)
    {
        return _dbContext.Roles.Find(id);
    }

    public List<Role> GetAll()
    {
        return _dbContext.Roles.ToList();
    }

    public bool Create(Role entity)
    {
        _dbContext.Add(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Update(Role entity)
    {
        _dbContext.Update(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Delete(Role entity)
    {
        _dbContext.Remove(entity);
        _dbContext.SaveChanges();
        return true;
    }
}