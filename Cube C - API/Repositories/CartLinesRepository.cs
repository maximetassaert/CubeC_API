using Cube_C___API.Models;
namespace Cube_C___API.Repositories;
using Microsoft.EntityFrameworkCore;

public class CartLinesRepository : IDisposable
{
    private readonly AppContext _context;

    public CartLinesRepository(AppContext context)
    {
        _context = context;
    }

    public IEnumerable<CartLine> FindAll()
    {
        return _context.CartLines.ToList();
    }

    public CartLine FindById(int id)
    {
        return _context.CartLines.Find(id);
    }

    public void Insert(CartLine cartLine)
    {
        _context.CartLines.Add(cartLine);
    }

    public void Delete(int id)
    {
        CartLine CartLine = _context.CartLines.Find(id);
        _context.CartLines.Remove(CartLine);
    }

    public void Update(CartLine cartLine)
    {
        _context.Entry(cartLine).State = EntityState.Modified;
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