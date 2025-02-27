using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesShop.Migrations
{
    /// <inheritdoc />
    public partial class Product2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryCateID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryCateID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryCateID",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CateId",
                table: "Products",
                column: "CateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CateId",
                table: "Products",
                column: "CateId",
                principalTable: "Categories",
                principalColumn: "CateID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CateId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CateId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "CategoryCateID",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryCateID",
                table: "Products",
                column: "CategoryCateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryCateID",
                table: "Products",
                column: "CategoryCateID",
                principalTable: "Categories",
                principalColumn: "CateID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
