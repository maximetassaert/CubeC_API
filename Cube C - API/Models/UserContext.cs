namespace Cube_C___API.Models;
using Microsoft.EntityFrameworkCore;


public class UserContext : DbContext
{
        public UserContext(DbContextOptions<UserContext> options) : base(options) {}
        public DbSet<User> Users { get; set; } = null!;
}