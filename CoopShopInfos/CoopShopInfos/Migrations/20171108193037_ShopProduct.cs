using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoopShopInfos.Migrations
{
    public partial class ShopProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShopProductId",
                table: "ShopProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ShopProduct_ShopProductId",
                table: "ShopProduct",
                column: "ShopProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ShopProduct_ShopProductId",
                table: "ShopProduct");

            migrationBuilder.DropColumn(
                name: "ShopProductId",
                table: "ShopProduct");
        }
    }
}
