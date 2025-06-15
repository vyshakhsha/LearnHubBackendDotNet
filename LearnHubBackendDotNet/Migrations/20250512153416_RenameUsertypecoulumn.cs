using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnHubBackendDotNet.Migrations
{
    public partial class RenameUsertypecoulumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
            name: "userType",          
            table: "Users",            
            newName: "usertype");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
            name: "userType",          
            table: "Users",            
            newName: "usertype");
        }

    }
}
