using Microsoft.EntityFrameworkCore;

namespace Carvalhal.View.Models
{
    public class CarvalhalContext : DbContext
    {
        public CarvalhalContext (DbContextOptions<CarvalhalContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
    }
}
