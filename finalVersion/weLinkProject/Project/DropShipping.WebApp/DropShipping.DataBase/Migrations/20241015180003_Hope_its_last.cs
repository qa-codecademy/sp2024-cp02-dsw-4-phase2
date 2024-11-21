using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DropShipping.DataBase.Migrations
{
    public partial class Hope_its_last : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProducts_Product_ProductsId",
                table: "UserProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProducts_User_UsersId",
                table: "UserProducts");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "UserProducts",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "UserProducts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProducts_UsersId",
                table: "UserProducts",
                newName: "IX_UserProducts_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProducts_Product_ProductId",
                table: "UserProducts",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProducts_User_UserId",
                table: "UserProducts",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProducts_Product_ProductId",
                table: "UserProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProducts_User_UserId",
                table: "UserProducts");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "UserProducts",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserProducts",
                newName: "ProductsId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProducts_ProductId",
                table: "UserProducts",
                newName: "IX_UserProducts_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProducts_Product_ProductsId",
                table: "UserProducts",
                column: "ProductsId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProducts_User_UsersId",
                table: "UserProducts",
                column: "UsersId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
