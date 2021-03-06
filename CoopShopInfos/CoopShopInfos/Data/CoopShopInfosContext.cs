using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoopShopInfos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CoopShopInfos.Models
{
    public class CoopShopInfosContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public CoopShopInfosContext (DbContextOptions<CoopShopInfosContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<ShopProduct> ShopProduct { get; set; }
        public DbSet<CategoryProduct> CategoryProduct { get; set; }
        public DbSet<CoopShopInfos.Models.Category> Category { get; set; }
        public DbSet<CoopShopInfos.Models.Price> Price { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShopProduct>()
                .HasKey(sp => new { sp.ShopId, sp.ProductId, sp.PriceId });

            modelBuilder.Entity<CategoryProduct>()
                .HasKey(cp => new { cp.CategoryId, cp.ProductId });

            //modelBuilder.Entity<ShopProduct>()
            //    .HasOne(sp => sp.Product)
            //    .WithMany(s => s.ShopProduct)
            //    .HasForeignKey(sp => sp.Id);

            //modelBuilder.Entity<ShopProduct>()
            //    .HasOne(sp => sp.Shop)
            //    .WithMany(s => s.ShopProduct)
            //    .HasForeignKey(sp => sp.ShopId);
        }

        

    }
}
