using Cube_C___API.Models;
using Microsoft.EntityFrameworkCore;
using AppContext = Cube_C___API.AppContext;

namespace Cube_C___API.Repositories;


public class SuppliersRepository : IDisposable
{
    private readonly AppContext _context;
    
    public SuppliersRepository(AppContext context)
    {
        _context = context;
    }
    public IEnumerable<Supplier> FindAll()
    {
        return _context.Suppliers.ToList();
    }
    public Supplier FindById(int id)
    {
        return _context.Suppliers.Find(id);
    }
    public void Insert(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
    }
    public void Delete(int id)
    {
        Supplier supplier = _context.Suppliers.Find(id);
        _context.Suppliers.Remove(supplier);
    }
    public void Update(Supplier supplier)
    {
        _context.Entry(supplier).State = EntityState.Modified;
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


