using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Othello_Web.Data.Migrations
{
    public partial class InitialDesign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OthelloOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    CurrentPlayer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OthelloOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OthelloGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameOverAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Player1Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Player1Type = table.Column<int>(type: "int", nullable: false),
                    Player2Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Player2Type = table.Column<int>(type: "int", nullable: false),
                    OthelloOptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OthelloGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OthelloGames_OthelloOptions_OthelloOptionId",
                        column: x => x.OthelloOptionId,
                        principalTable: "OthelloOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OthelloGamesStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SerializedGameState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AxisX = table.Column<int>(type: "int", nullable: false),
                    AxisY = table.Column<int>(type: "int", nullable: false),
                    BlackScore = table.Column<int>(type: "int", nullable: false),
                    WhiteScore = table.Column<int>(type: "int", nullable: false),
                    Winner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OthelloGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OthelloGamesStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OthelloGamesStates_OthelloGames_OthelloGameId",
                        column: x => x.OthelloGameId,
                        principalTable: "OthelloGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OthelloGames_OthelloOptionId",
                table: "OthelloGames",
                column: "OthelloOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_OthelloGamesStates_OthelloGameId",
                table: "OthelloGamesStates",
                column: "OthelloGameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OthelloGamesStates");

            migrationBuilder.DropTable(
                name: "OthelloGames");

            migrationBuilder.DropTable(
                name: "OthelloOptions");
        }
    }
}
