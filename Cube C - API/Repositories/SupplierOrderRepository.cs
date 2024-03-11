using Cube_C___API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cube_C___API.Repositories;

public class SupplierOrderRepository : BaseRepository, IRepositoryData<SupplierOrder>
{
    public SupplierOrderRepository
        (ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public SupplierOrder GetById(int id)
    {
        return _dbContext.SupplierOrders.Include(supplierOrder => supplierOrder.SupplierOrderLines)
            .ThenInclude(supplierOrderLine => supplierOrderLine.Product).ToList().Find(supplierOrder => supplierOrder.Id == id);
    }

    public List<SupplierOrder> GetAll()
    {
        return _dbContext.SupplierOrders.Include(supplierOrder => supplierOrder.SupplierOrderLines)
            .ThenInclude(supplierOrderLine => supplierOrderLine.Product).ToList();
    }

    public bool Create(SupplierOrder entity)
    {
        _dbContext.Add(entity);
        _dbContext.SaveChanges();
        return true;
    }

    bool IRepositoryData<SupplierOrder>.Update(SupplierOrder entity)
    {
        _dbContext.Update(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public bool Delete(SupplierOrder entity)
    {
        _dbContext.Remove(entity);
        _dbContext.SaveChanges();
        return true;
    }

    public void Update(SupplierOrder supplierOrder)
    {
        _dbContext.Update(supplierOrder);
        _dbContext.SaveChanges();
    }
}