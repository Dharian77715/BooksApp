using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksApp.Migrations
{
    public partial class AddsexName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SexId",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SexId",
                table: "Authors");
        }
    }
}
