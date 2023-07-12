using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StufShop.Migrations
{
    public partial class Dateupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PayedCartItemID",
                table: "CartPayeds",
                newName: "CartPayedItemID");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "CartPayeds",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CartPayedItemID",
                table: "CartPayeds",
                newName: "PayedCartItemID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "CartPayeds",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
