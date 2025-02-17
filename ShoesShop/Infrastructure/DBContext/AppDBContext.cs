using API_ShoesShop.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Domain.Entities;

namespace API_ShoesShop.Infrastructure.DBContext
{
    public class AppDBContext : IdentityDbContext<ApplicationUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductDetail> ProductDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductDetail>()
                .HasOne(pd => pd.Color)
                .WithMany()
                .HasForeignKey(pd => pd.ColorId);

            modelBuilder.Entity<ProductDetail>()
                .HasOne(pd => pd.Size)
                .WithMany()
                .HasForeignKey(pd => pd.SizeId);
        }

    }


}
