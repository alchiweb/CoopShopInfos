using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoopShopInfos.Migrations
{
    public partial class DateTimeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceDateTime",
                table: "ShopProduct");

            migrationBuilder.AddColumn<DateTime>(
                name: "PriceDateTime",
                table: "Price",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceDateTime",
                table: "Price");

            migrationBuilder.AddColumn<DateTime>(
                name: "PriceDateTime",
                table: "ShopProduct",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
