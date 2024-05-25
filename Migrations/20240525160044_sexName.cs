using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksApp.Migrations
{
    public partial class sexName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Authors_SexId",
                table: "Authors",
                column: "SexId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Sexes_SexId",
                table: "Authors",
                column: "SexId",
                principalTable: "Sexes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Sexes_SexId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_SexId",
                table: "Authors");
        }
    }
}
