using Cube_C___API.Models;
using Microsoft.EntityFrameworkCore;
using AppContext = Cube_C___API.AppContext;

namespace Cube_C___API.Repositories;

public class CategoriesRepository : IDisposable
{
    private readonly AppContext _context;
    
    public CategoriesRepository(AppContext context)
    {
        _context = context;
    }
    public IEnumerable<Category> FindAll()
    {
        return _context.Categories.ToList();
    }
    public Category FindById(int id)
    {
        return _context.Categories.Find(id);
    }
    public void Insert(Category category)
    {
        _context.Categories.Add(category);
    }
    public void Delete(int id)
    {
        Category category = _context.Categories.Find(id);
        _context.Categories.Remove(category);
    }
    public void Update(Category category)
    {
        _context.Entry(category).State = EntityState.Modified;
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
