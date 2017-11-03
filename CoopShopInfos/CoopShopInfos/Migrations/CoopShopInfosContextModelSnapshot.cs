﻿// <auto-generated />
using CoopShopInfos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CoopShopInfos.Migrations
{
    [DbContext(typeof(CoopShopInfosContext))]
    partial class CoopShopInfosContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoopShopInfos.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Barcode")
                        .IsRequired()
                        .HasMaxLength(13);

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ProductId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("CoopShopInfos.Models.Shop", b =>
                {
                    b.Property<int>("ShopId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ShopAddress")
                        .HasMaxLength(100);

                    b.Property<string>("ShopCity")
                        .HasMaxLength(100);

                    b.Property<string>("ShopName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("ShopPhoneNumber");

                    b.Property<string>("ShopPostCode")
                        .HasMaxLength(5);

                    b.HasKey("ShopId");

                    b.ToTable("Shop");
                });

            modelBuilder.Entity("CoopShopInfos.Models.ShopProduct", b =>
                {
                    b.Property<int>("ShopId");

                    b.Property<int>("Id");

                    b.Property<decimal>("Price");

                    b.Property<int?>("ProductId");

                    b.HasKey("ShopId", "Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ShopProduct");
                });

            modelBuilder.Entity("CoopShopInfos.Models.ShopProduct", b =>
                {
                    b.HasOne("CoopShopInfos.Models.Product", "Product")
                        .WithMany("ShopProduct")
                        .HasForeignKey("ProductId");

                    b.HasOne("CoopShopInfos.Models.Shop", "Shop")
                        .WithMany("ShopProduct")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
