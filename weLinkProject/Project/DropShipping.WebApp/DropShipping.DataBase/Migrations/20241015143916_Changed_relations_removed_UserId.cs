using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DropShipping.DataBase.Migrations
{
    public partial class Changed_relations_removed_UserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "SortOrder", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "SortOrder", "UserId" },
                values: new object[] { 2, 2 });
        }
    }
}
