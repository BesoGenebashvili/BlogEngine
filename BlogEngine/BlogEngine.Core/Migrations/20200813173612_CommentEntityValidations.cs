using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogEngine.Core.Migrations
{
    public partial class CommentEntityValidations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Comments",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Comments",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "Content",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
