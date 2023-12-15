using Cube_C___API.Models;
using Microsoft.EntityFrameworkCore;
using AppContext = Cube_C___API.AppContext;

namespace Cube_C___API.Repositories;

public class UsersRepository : IDisposable
{
    private readonly AppContext _context;
    
    public UsersRepository(AppContext context)
    {
        _context = context;
    }
    public IEnumerable<User> FindAll()
    {
        return _context.Users
            .Include(user => user.Roles)
            .ToList();
    }
    public User? FindById(int id)
    {
        return _context.Users
            .Include(user => user.Roles)
            // .Find(id);
            .FirstOrDefault(user => user.Id == id);
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
