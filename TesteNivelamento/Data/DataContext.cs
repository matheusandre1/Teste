using Microsoft.EntityFrameworkCore;
using TesteNivelamento.Models;

namespace TesteNivelamento.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options ) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
