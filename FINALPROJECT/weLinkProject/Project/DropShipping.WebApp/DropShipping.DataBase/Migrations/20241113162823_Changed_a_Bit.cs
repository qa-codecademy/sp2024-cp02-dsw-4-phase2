using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DropShipping.DataBase.Migrations
{
    public partial class Changed_a_Bit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Orders_OrdersId",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Product_ProductsId",
                table: "OrderProduct");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "OrderProduct",
                newName: "productsId");

            migrationBuilder.RenameColumn(
                name: "OrdersId",
                table: "OrderProduct",
                newName: "ordersId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProduct_ProductsId",
                table: "OrderProduct",
                newName: "IX_OrderProduct_productsId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Orders_ordersId",
                table: "OrderProduct",
                column: "ordersId",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Product_productsId",
                table: "OrderProduct",
                column: "productsId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Orders_ordersId",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Product_productsId",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderProduct");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Orders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "productsId",
                table: "OrderProduct",
                newName: "ProductsId");

            migrationBuilder.RenameColumn(
                name: "ordersId",
                table: "OrderProduct",
                newName: "OrdersId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProduct_productsId",
                table: "OrderProduct",
                newName: "IX_OrderProduct_ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Orders_OrdersId",
                table: "OrderProduct",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Product_ProductsId",
                table: "OrderProduct",
                column: "ProductsId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
