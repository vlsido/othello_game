using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Othello_Web.Data.Migrations
{
    public partial class currentMoveByBlack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CurrentMoveByBlack",
                table: "OthelloGamesStates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentMoveByBlack",
                table: "OthelloGamesStates");
        }
    }
}
