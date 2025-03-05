using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesShop.Migrations
{
    /// <inheritdoc />
    public partial class updateProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CateId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CateId",
                table: "Products",
                newName: "CateID");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Products",
                newName: "BrandID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CateId",
                table: "Products",
                newName: "IX_Products_CateID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                newName: "IX_Products_BrandID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandID",
                table: "Products",
                column: "BrandID",
                principalTable: "Brands",
                principalColumn: "BrandID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CateID",
                table: "Products",
                column: "CateID",
                principalTable: "Categories",
                principalColumn: "CateID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CateID",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CateID",
                table: "Products",
                newName: "CateId");

            migrationBuilder.RenameColumn(
                name: "BrandID",
                table: "Products",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CateID",
                table: "Products",
                newName: "IX_Products_CateId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandID",
                table: "Products",
                newName: "IX_Products_BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CateId",
                table: "Products",
                column: "CateId",
                principalTable: "Categories",
                principalColumn: "CateID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
