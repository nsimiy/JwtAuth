using JwtAuth.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtAuth.Data
{
    public class JwtDbContext : DbContext
    {
            public JwtDbContext(DbContextOptions options) : base(options) 
            {
            
            }

            public DbSet<User> fire { get; set; }
    }
}
