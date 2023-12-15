using Cube_C___API.Models;
using Microsoft.EntityFrameworkCore;
using AppContext = Cube_C___API.AppContext;

namespace Cube_C___API.Repositories;

public class RolesRepository
{
    private readonly AppContext _context;
    
    public RolesRepository(AppContext context)
    {
        _context = context;
    }
    public IEnumerable<Roles> FindAll()
    {
        return _context.Roles.ToList();
    }
    public Roles FindById(int id)
    {
        return _context.Roles.Find(id);
    }
    public void Insert(Roles supplier)
    {
        _context.Roles.Add(roles);
    }
    public void Delete(int id)
    {
        Roles roles = _context.Roles.Find(id);
        _context.Roles.Remove(supplier);
    }
    public void Update(Roles roles)
    {
        _context.Entry(roles).State = EntityState.Modified;
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