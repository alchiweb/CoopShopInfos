using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoopShopInfos.Models;

namespace CoopShopInfos.Models
{
    public class CoopShopInfosContext : DbContext
    {
        public CoopShopInfosContext (DbContextOptions<CoopShopInfosContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<ShopProduct> ShopProduct { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShopProduct>()
                .HasKey(sp => new { sp.ShopId, ProductId = sp.Id });

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
