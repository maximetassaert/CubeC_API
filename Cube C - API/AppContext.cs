using Cube_C___API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cube_C___API;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Supplier> Suppliers { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Cart> Carts { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<CartLine> CartLines { get; set; } = null!;

    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<SupplierOrder> SupplierOrders { get; set; } = null!;
    public DbSet<SupplierOrderLine> SupplierOrderLines { get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}