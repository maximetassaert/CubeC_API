namespace Cube_C___API;
using Microsoft.EntityFrameworkCore;
using Cube_C___API.Models;

public class AppContext : DbContext
{
        public AppContext(DbContextOptions<AppContext> options) : base(options) {}
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

}