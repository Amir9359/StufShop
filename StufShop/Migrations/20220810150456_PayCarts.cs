using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StufShop.Migrations
{
    public partial class PayCarts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartPayeds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Paynumber = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    PayedCartItemID = table.Column<int>(nullable: false),
                    CusstomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartPayeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartPayeds_AspNetUsers_CusstomerId",
                        column: x => x.CusstomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartItemPayeds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    PayedCartid = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    PayedCartsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemPayeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItemPayeds_CartPayeds_PayedCartsId",
                        column: x => x.PayedCartsId,
                        principalTable: "CartPayeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItemPayeds_PayedCartsId",
                table: "CartItemPayeds",
                column: "PayedCartsId");

            migrationBuilder.CreateIndex(
                name: "IX_CartPayeds_CusstomerId",
                table: "CartPayeds",
                column: "CusstomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItemPayeds");

            migrationBuilder.DropTable(
                name: "CartPayeds");
        }
    }
}
