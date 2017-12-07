using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoopShopInfos.Migrations
{
    public partial class PriceWithTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ShopProduct");

            migrationBuilder.DropColumn(
                name: "Qauntity",
                table: "Product");

            migrationBuilder.AddColumn<DateTime>(
                name: "PriceDateTime",
                table: "ShopProduct",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "ShopProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Quantity",
                table: "Product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    PriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PriceAmount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.PriceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopProduct_PriceId",
                table: "ShopProduct",
                column: "PriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProduct_Price_PriceId",
                table: "ShopProduct",
                column: "PriceId",
                principalTable: "Price",
                principalColumn: "PriceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopProduct_Price_PriceId",
                table: "ShopProduct");

            migrationBuilder.DropTable(
                name: "Price");

            migrationBuilder.DropIndex(
                name: "IX_ShopProduct_PriceId",
                table: "ShopProduct");

            migrationBuilder.DropColumn(
                name: "PriceDateTime",
                table: "ShopProduct");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "ShopProduct");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Product");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ShopProduct",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<float>(
                name: "Qauntity",
                table: "Product",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
