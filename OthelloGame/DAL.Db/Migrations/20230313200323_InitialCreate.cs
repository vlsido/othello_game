using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Db.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OthelloOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Width = table.Column<int>(type: "INTEGER", nullable: false),
                    Height = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentPlayer = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OthelloOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OthelloGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GameOverAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Player1Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Player1Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Player2Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Player2Type = table.Column<int>(type: "INTEGER", nullable: false),
                    OthelloOptionId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SerializedGameState = table.Column<string>(type: "TEXT", nullable: false),
                    Perspective = table.Column<string>(type: "TEXT", nullable: true),
                    AxisX = table.Column<int>(type: "INTEGER", nullable: false),
                    AxisY = table.Column<int>(type: "INTEGER", nullable: false),
                    BlackScore = table.Column<int>(type: "INTEGER", nullable: false),
                    WhiteScore = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentMoveByBlack = table.Column<bool>(type: "INTEGER", nullable: false),
                    Winner = table.Column<string>(type: "TEXT", nullable: true),
                    OthelloGameId = table.Column<int>(type: "INTEGER", nullable: false)
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
