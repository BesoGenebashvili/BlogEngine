using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogEngine.Core.Migrations
{
    public partial class CommentSystemRefactored : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogComments");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.CreateTable(
                name: "MainComments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    BlogID = table.Column<int>(nullable: false),
                    Body = table.Column<string>(maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainComments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MainComments_Blogs_BlogID",
                        column: x => x.BlogID,
                        principalTable: "Blogs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubComments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateBy = table.Column<string>(maxLength: 50, nullable: true),
                    BlogID = table.Column<int>(nullable: false),
                    Body = table.Column<string>(maxLength: 300, nullable: false),
                    MainCommentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubComments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubComments_MainComments_MainCommentID",
                        column: x => x.MainCommentID,
                        principalTable: "MainComments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MainComments_BlogID",
                table: "MainComments",
                column: "BlogID");

            migrationBuilder.CreateIndex(
                name: "IX_SubComments_MainCommentID",
                table: "SubComments",
                column: "MainCommentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubComments");

            migrationBuilder.DropTable(
                name: "MainComments");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Edited = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BlogComments",
                columns: table => new
                {
                    BlogID = table.Column<int>(type: "int", nullable: false),
                    CommentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogComments", x => new { x.BlogID, x.CommentID });
                    table.ForeignKey(
                        name: "FK_BlogComments_Blogs_BlogID",
                        column: x => x.BlogID,
                        principalTable: "Blogs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogComments_Comments_CommentID",
                        column: x => x.CommentID,
                        principalTable: "Comments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_CommentID",
                table: "BlogComments",
                column: "CommentID");
        }
    }
}
