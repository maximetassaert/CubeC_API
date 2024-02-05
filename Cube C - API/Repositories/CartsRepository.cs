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
    
    public Cart? FindCartByCustomer(int customerId)
    {
        return _context.Carts.Include(cart => cart.CartLines).ThenInclude(cartLine => cartLine.Product).FirstOrDefault(cart => cart.CustomerId == customerId && cart.Editable);
    }

    public Cart? FindById(int id)
    {
        return _context.Carts.Include(cart => cart.CartLines).ThenInclude(cartLine => cartLine.Product).FirstOrDefault(cart => cart.Id == id);
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

    public void Update(Cart cartToUpdate)
    {
        //_context.Entry(cartToUpdate).State = EntityState.Modified;
        // soit ça maj cart update soit l'autre mais 2 en même temps ça ne marche pas
        //_context.SaveChanges();

        var existingCart = _context.Carts.Include(cart => cart.CartLines).FirstOrDefault(cart => cart.Id == cartToUpdate.Id);
        if (existingCart != null)
        {
            // Delete children
            foreach (var existingChild in existingCart.CartLines.ToList())
            {
                if (cartToUpdate.CartLines.All(c => c.Id != existingChild.Id))
                    _context.CartLines.Remove(existingChild);
            }

            // Update and Insert children
            foreach (var childModel in cartToUpdate.CartLines)
            {
                var existingChild = existingCart.CartLines
                    .FirstOrDefault(c => c.Id == childModel.Id);

                if (existingChild != null)
                {
                    // Update child
                    existingChild.Quantity = childModel.Quantity;
                    _context.Entry(existingChild).State = EntityState.Modified;
                }
                else
                {
                    // Insert child
                    existingCart.CartLines.Add(childModel);
                }
            }

        }

        _context.SaveChanges();
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