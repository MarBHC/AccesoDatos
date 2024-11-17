using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Membresia> Membresia { get; set; }
        public DbSet<Pago> pagos { get; set; }
    }
}
