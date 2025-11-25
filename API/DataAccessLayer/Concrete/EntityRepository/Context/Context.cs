using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Concrete.EntityRepository.Context
{
    public class Context : DbContext
    {

        public Context(DbContextOptions options) : base(options)
        {

        }
        /*
        private readonly IConfiguration _configuration;

        public Context(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("SQLServerConnection");
                // SQL Server için eski bağlantı
                 optionsBuilder.UseSqlServer(connectionString);

                // PostgreSQL için yeni bağlantı
                //optionsBuilder.UseNpgsql(connectionString);
            }
        }
*/
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }

        public DbSet<Address> Address { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Order - Kullanici ilişkisi
           //modelBuilder.Entity<Order>()
           //    .HasOne(o => o.Kullanici)
           //    .WithMany()
           //    .HasForeignKey(o => o.KullaniciId)
           //    .OnDelete(DeleteBehavior.Cascade);

            // Review - Kullanici ilişkisi
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Kullanici)
                .WithMany()
                .HasForeignKey(r => r.KullaniciId)
                .OnDelete(DeleteBehavior.Cascade);

            // Basket - Kullanici ilişkisi (NotMapped olduğu için konfigürasyon eklenmedi)

            // Product - Category ilişkisi
            modelBuilder.Entity<Product>()
                .HasOne<Category>()
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Address tablosu için ekstra konfigürasyon (isteğe bağlı)
            modelBuilder.Entity<Address>()
                .Property(a => a.KullaniciId)
                .IsRequired();

            // Payment - Order ilişkisi (OrderId nullable)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany()
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
        }

    }
}
