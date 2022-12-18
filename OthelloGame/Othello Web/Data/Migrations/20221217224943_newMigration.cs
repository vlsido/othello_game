using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Othello_Web.Data.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AxisX",
                table: "OthelloGamesStates");

            migrationBuilder.DropColumn(
                name: "AxisY",
                table: "OthelloGamesStates");

            migrationBuilder.AddColumn<string>(
                name: "Perspective",
                table: "OthelloGamesStates",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Perspective",
                table: "OthelloGamesStates");

            migrationBuilder.AddColumn<int>(
                name: "AxisX",
                table: "OthelloGamesStates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AxisY",
                table: "OthelloGamesStates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
