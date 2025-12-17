using cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace cadastro.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }  
    }
}