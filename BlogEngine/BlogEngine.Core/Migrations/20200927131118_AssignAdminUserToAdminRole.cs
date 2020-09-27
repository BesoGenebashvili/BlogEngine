using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogEngine.Core.Migrations
{
    public partial class AssignAdminUserToAdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string insertSQLScriptPath = AppDomain.CurrentDomain.BaseDirectory + @"SQLScripts\InsertAdminRoleClaimToAdminUserSQLQuery.sql";

            string insertSQLScript = File.ReadAllText(insertSQLScriptPath);

            migrationBuilder.Sql(insertSQLScript);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string deleteSQLScriptPath = AppDomain.CurrentDomain.BaseDirectory + @"SQLScripts\DeleteAdminRoleClaimToAdminUserSQLQuery.sql";

            string deleteSQLScript = File.ReadAllText(deleteSQLScriptPath);

            migrationBuilder.Sql(deleteSQLScript);
        }
    }
}