using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogEngine.Core.Migrations
{
    public partial class NotificationReceiversAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationReceivers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    EmailAddress = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationReceivers", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationReceivers");
        }
    }
}
