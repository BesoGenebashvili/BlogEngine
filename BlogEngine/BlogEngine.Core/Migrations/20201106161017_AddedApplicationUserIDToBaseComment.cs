using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogEngine.Core.Migrations
{
    public partial class AddedApplicationUserIDToBaseComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserID",
                table: "SubComments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserID",
                table: "MainComments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserID",
                table: "SubComments");

            migrationBuilder.DropColumn(
                name: "ApplicationUserID",
                table: "MainComments");
        }
    }
}
