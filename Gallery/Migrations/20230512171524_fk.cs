using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gallery.Migrations
{
    /// <inheritdoc />
    public partial class fk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GamesGenre_GameId",
                table: "GamesGenre",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesGenre_GenreId",
                table: "GamesGenre",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamesGenre_Game_GameId",
                table: "GamesGenre",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamesGenre_Genre_GenreId",
                table: "GamesGenre",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamesGenre_Game_GameId",
                table: "GamesGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_GamesGenre_Genre_GenreId",
                table: "GamesGenre");

            migrationBuilder.DropIndex(
                name: "IX_GamesGenre_GameId",
                table: "GamesGenre");

            migrationBuilder.DropIndex(
                name: "IX_GamesGenre_GenreId",
                table: "GamesGenre");
        }
    }
}
