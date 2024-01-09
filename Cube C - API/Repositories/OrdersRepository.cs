using Cube_C___API.Models;
using Microsoft.EntityFrameworkCore;
using AppContext = Cube_C___API.AppContext;

namespace Cube_C___API.Repositories;


public class OrdersRepository : IDisposable
{
    private readonly AppContext _context;

    public OrdersRepository(AppContext context)
    {
        _context = context;
    }

    public IEnumerable<Order> FindAll()
    {
        return _context.Orders.ToList();
    }

    public Order FindById(int id)
    {
        return _context.Orders.Find(id);
    }

    public void Insert(Order order)
    {
        _context.Orders.Add(order);
    }

    public void Delete(int id)
    {
        Order order = _context.Orders.Find(id);
        _context.Orders.Remove(order);
    }

    public void Update(Order order)
    {
        _context.Entry(order).State = EntityState.Modified;
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
