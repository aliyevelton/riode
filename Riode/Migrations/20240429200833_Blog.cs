using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Riode.Migrations
{
    public partial class Blog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Blogs_BlogId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_BlogId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "ProductImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "ProductImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_BlogId",
                table: "ProductImages",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Blogs_BlogId",
                table: "ProductImages",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id");
        }
    }
}
