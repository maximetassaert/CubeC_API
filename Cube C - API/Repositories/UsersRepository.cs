using Cube_C___API.Dtos.Role;
using Cube_C___API.Dtos.User;
using Cube_C___API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cube_C___API.Repositories;

public class UsersRepository : IDisposable
{
    private readonly AppContext _context;
    
    public UsersRepository(AppContext context)
    {
        _context = context;
    }
    public IEnumerable<GetUserDto> FindAll()
    {
        // return _context.Users
        //     .Include(user => user.Roles)
        //     .ToList();
        return _context.Users
            .Include(user => user.Roles)
            .Select(user => new GetUserDto
            {
                Id = user.Id,
                Mail = user.Mail,
                Roles = user.Roles.Select(role => new GetRoleDto { Name = role.Name}).ToList()
            })
            .ToList();
        }
    
    public GetUserDto? FindById(int id)
    {
        return _context.Users
            .Include(user => user.Roles)
            // .Find(id);
            .Where(user => user.Id == id )
            .Select(user => new GetUserDto
            {
                Id = user.Id,
                Mail = user.Mail,
                Roles = user.Roles.Select(role => new GetRoleDto { Name = role.Name}).ToList()
            })
            .FirstOrDefault(user => user.Id == id);
    }
    
    public User? findByEmail(string mail)
    {
        return _context.Users
            .Include(user => user.Roles)
            .FirstOrDefault(user => user.Mail == mail);
    }
    public void Insert(User user)
    {
        _context.Users.Add(user);
    }
    public void Delete(int id)
    {
        User user = _context.Users.Find(id);
        _context.Users.Remove(user);
    }
    public void Update(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }
    public void Save()
    {
        _context.SaveChanges();
    }
    private bool disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
