using Microsoft.EntityFrameworkCore.Migrations;

namespace StufShop.Migrations
{
    public partial class PayCartsEntitys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItemPayeds_CartPayeds_PayedCartsId",
                table: "CartItemPayeds");

            migrationBuilder.DropIndex(
                name: "IX_CartItemPayeds_PayedCartsId",
                table: "CartItemPayeds");

            migrationBuilder.DropColumn(
                name: "PayedCartsId",
                table: "CartItemPayeds");

            migrationBuilder.RenameColumn(
                name: "PayedCartid",
                table: "CartItemPayeds",
                newName: "CartPayedId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItemPayeds_CartPayedId",
                table: "CartItemPayeds",
                column: "CartPayedId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItemPayeds_CartPayeds_CartPayedId",
                table: "CartItemPayeds",
                column: "CartPayedId",
                principalTable: "CartPayeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItemPayeds_CartPayeds_CartPayedId",
                table: "CartItemPayeds");

            migrationBuilder.DropIndex(
                name: "IX_CartItemPayeds_CartPayedId",
                table: "CartItemPayeds");

            migrationBuilder.RenameColumn(
                name: "CartPayedId",
                table: "CartItemPayeds",
                newName: "PayedCartid");

            migrationBuilder.AddColumn<int>(
                name: "PayedCartsId",
                table: "CartItemPayeds",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItemPayeds_PayedCartsId",
                table: "CartItemPayeds",
                column: "PayedCartsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItemPayeds_CartPayeds_PayedCartsId",
                table: "CartItemPayeds",
                column: "PayedCartsId",
                principalTable: "CartPayeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
