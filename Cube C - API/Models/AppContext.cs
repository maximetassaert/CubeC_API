namespace Cube_C___API.Models;
using Microsoft.EntityFrameworkCore;


public class AppContext : DbContext
{
        public AppContext(DbContextOptions<AppContext> options) : base(options) {}
        public DbSet<User> Users { get; set; } = null!;
}