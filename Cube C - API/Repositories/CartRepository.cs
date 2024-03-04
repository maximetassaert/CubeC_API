using Cube_C___API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cube_C___API.Repositories;

public class CartRepository : BaseRepository, IRepositoryData<Cart>
{
    public CartRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Cart GetById(int id)
    {
        return _dbContext.Carts.Include(cart => cart.CartLines).ThenInclude(cartLine => cartLine.Product)
            .ThenInclude(product => product.Supplier)
            .FirstOrDefault(cart => cart.Id == id);
    }

    public List<Cart> GetAll()
    {
        return _dbContext.Carts.ToList();
    }

    public bool Create(Cart entity)
    {
        _dbContext.Add(entity);
        _dbContext.SaveChanges();
        return true;
    }

    bool IRepositoryData<Cart>.Update(Cart entity)
    {
        _dbContext.Update(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Delete(Cart entity)
    {
        _dbContext.Remove(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public Cart? FindCartByCustomer(int customerId)
    {
        return _dbContext.Carts.Include(cart => cart.CartLines).ThenInclude(cartLine => cartLine.Product)
            .FirstOrDefault(cart => cart.CustomerId == customerId && cart.Editable);
    }

    public void UpdateAll(Cart cartToUpdate)
    {
        //_context.Entry(cartToUpdate).State = EntityState.Modified;
        // soit ça maj cart update soit l'autre mais 2 en même temps ça ne marche pas
        //_context.SaveChanges();

        var existingCart = _dbContext.Carts.Include(cart => cart.CartLines)
            .FirstOrDefault(cart => cart.Id == cartToUpdate.Id);
        if (existingCart != null)
        {
            // Delete children
            foreach (var existingChild in existingCart.CartLines.ToList())
                if (cartToUpdate.CartLines.All(c => c.Id != existingChild.Id))
                    _dbContext.CartLines.Remove(existingChild);

            // Update and Insert children
            foreach (var childModel in cartToUpdate.CartLines)
            {
                var existingChild = existingCart.CartLines
                    .FirstOrDefault(c => c.Id == childModel.Id);

                if (existingChild != null)
                {
                    // Update child
                    existingChild.Quantity = childModel.Quantity;
                    _dbContext.Entry(existingChild).State = EntityState.Modified;
                }
                else
                {
                    // Insert child
                    existingCart.CartLines.Add(childModel);
                }
            }
        }

        _dbContext.SaveChanges();
    }
}