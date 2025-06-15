using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnHubBackendDotNet.Migrations
{
    public partial class AddusertypeToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "usertype",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "usertype",
                table: "Users");
        }
    }
}
