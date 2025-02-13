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
    }
}
