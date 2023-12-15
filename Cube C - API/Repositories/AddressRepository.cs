
using Cube_C___API.Models;
namespace Cube_C___API.Repositories;
using Microsoft.EntityFrameworkCore;

public class AddressRepository : IDisposable
{
    private readonly AppContext _context;

    public AddressRepository(AppContext context)
    {
        _context = context;
    }

    public IEnumerable<Address> FindAll()
    {
        return _context.Addresses.ToList();
    }

    public Address FindById(int id)
    {
        return _context.Addresses.Find(id);
    }

    public void Insert(Address address)
    {
        _context.Addresses.Add(address);
    }

    public void Delete(int id)
    {
        Address address = _context.Addresses.Find(id);
        _context.Addresses.Remove(address);
    }

    public void Update(Address address)
    {
        _context.Entry(address).State = EntityState.Modified;
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