using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoopShopInfos.Migrations
{
    public partial class ShopProductKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopProduct_Product_ProductId",
                table: "ShopProduct");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ShopProduct_ShopProductId",
                table: "ShopProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopProduct",
                table: "ShopProduct");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShopProduct");

            migrationBuilder.DropColumn(
                name: "ShopProductId",
                table: "ShopProduct");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ShopProduct",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopProduct",
                table: "ShopProduct",
                columns: new[] { "ShopId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProduct_Product_ProductId",
                table: "ShopProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopProduct_Product_ProductId",
                table: "ShopProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopProduct",
                table: "ShopProduct");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ShopProduct",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ShopProduct",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShopProductId",
                table: "ShopProduct",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ShopProduct_ShopProductId",
                table: "ShopProduct",
                column: "ShopProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopProduct",
                table: "ShopProduct",
                columns: new[] { "ShopId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProduct_Product_ProductId",
                table: "ShopProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
