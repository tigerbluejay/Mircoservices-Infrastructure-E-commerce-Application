using Discount.gRPC.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data
{
    public class DiscountContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;

        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasKey(c => c.Id);
            modelBuilder.Entity<Coupon>().Property(c => c.ProductName).IsRequired();
            modelBuilder.Entity<Coupon>().Property(c => c.Description).IsRequired();
            modelBuilder.Entity<Coupon>().Property(c => c.Amount).IsRequired();

            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 },
                new Coupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 100 }
            );
        }
    }
}
