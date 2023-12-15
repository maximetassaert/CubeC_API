using Cube_C___API.Models;
namespace Cube_C___API.Repositories;
using Microsoft.EntityFrameworkCore;

public class ProductsRepository :IDisposable
{
    private readonly AppContext _context;

    public ProductsRepository(AppContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> FindAll()
    {
        return _context.Products.ToList();
    }

    public Product FindById(int id)
    {
        return _context.Products.Find(id);
    }

    public void Insert(Product product)
    {
        _context.Products.Add(product);
    }

    public void Delete(int id)
    {
        Product product = _context.Products.Find(id);
        _context.Products.Remove(product);
    }

    public void Update(Product product)
    {
        _context.Products.Entry(product).State = EntityState.Modified;
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
        throw new NotImplementedException();
    }
}