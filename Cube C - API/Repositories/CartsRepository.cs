using Cube_C___API.Models;
namespace Cube_C___API.Repositories;
using Microsoft.EntityFrameworkCore;

public class CartsRepository : IDisposable
{
    private readonly AppContext _context;

    public CartsRepository(AppContext context)
    {
        _context = context;
    }

    public IEnumerable<Cart> FindAll()
    {
        return _context.Carts.ToList();
    }

    public Cart FindById(int id)
    {
        return _context.Carts.Find(id);
    }

    public void Insert(Cart cart)
    {
        _context.Carts.Add(cart);
    }

    public void Delete(int id)
    {
        Cart cart = _context.Carts.Find(id);
        _context.Carts.Remove(cart);
    }

    public void Update(Cart cart)
    {
        _context.Entry(cart).State = EntityState.Modified;
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