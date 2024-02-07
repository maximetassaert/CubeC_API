using Cube_C___API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cube_C___API.Repositories;

public class UserRepository : BaseRepository, IRepositoryData<User>
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }


    public User GetById(int id)
    {
        return _dbContext.Users.Find(id);
    }

    public List<User> GetAll()
    {
        return _dbContext.Users.ToList();
    }

    public bool Create(User entity)
    {
        _dbContext.Add(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Update(User entity)
    {
        _dbContext.Update(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Delete(User entity)
    {
        _dbContext.Remove(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public IEnumerable<User> FindAll()
    {
        // return _context.Users
        //     .Include(user => user.Roles)
        //     .ToList();
        return _dbContext.Users
            .Include(user => user.Roles)
            .ToList();


        //.Select(user => new GetUserDto
        // {
        //     Id = user.Id,
        //     Mail = user.Mail,
        //     Roles = user.Roles.Select(role => new GetRoleDto { Name = role.Name }).ToList()
        // })
    }

    public User? FindById(int id)
    {
        return _dbContext.Users
            .Include(user => user.Roles)
            // .Find(id);
            .Where(user => user.Id == id)
            .FirstOrDefault(user => user.Id == id);
    }

    public User? findByEmail(string mail)
    {
        return _dbContext.Users
            .Include(user => user.Roles)
            .FirstOrDefault(user => user.Mail == mail);
    }
}