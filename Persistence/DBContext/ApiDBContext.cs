using Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DBContext
{
    public class ApiDBContext : DbContext 
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            // Configuración de la cadena de conexión a la base de datos
            //var configuration = new ConfigurationBuilder()
            //        .Sources("appsettings.json")
            //        .Build();

            //var connectionString = configuration.GetConnectionString("conString");

            //optionsBuilder.UseSqlServer("connectionString");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 

            modelBuilder.Entity<Category>();

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18, 2)"); 

                entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRODUCTS-CATEGORY");
            });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName) 
                .IsUnique();


            // Adding some records to the tables

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Lacteos" },
                new Category { Id = 2, Name = "Gaseosas" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Price = 10.00m, LoadDate = DateTime.Parse("2024-05-16"), IdCategory = 1 },
                new Product { Id = 2, Price = 60.00m, LoadDate = DateTime.Parse("2024-05-16"), IdCategory = 2 },
                new Product { Id = 3, Price = 5.00m, LoadDate = DateTime.Parse("2024-05-16"), IdCategory = 2 },
                new Product { Id = 4, Price = 5.00m, LoadDate = DateTime.Parse("2024-05-16"), IdCategory = 1 },
                new Product { Id = 5, Price = 15.00m, LoadDate = DateTime.Parse("2024-05-16"), IdCategory = 2 }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UserName = "luciano", Password = "pass" },
                new User { Id = 2, UserName = "usuario2", Password = "contraseña2" }
            );
        }

    }
}
