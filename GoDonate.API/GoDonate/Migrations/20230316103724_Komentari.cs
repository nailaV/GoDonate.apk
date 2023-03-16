using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoDonate.Migrations
{
    /// <inheritdoc />
    public partial class Komentari : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "korisnikID",
                table: "Komentari",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Komentari_korisnikID",
                table: "Komentari",
                column: "korisnikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Komentari_Korisnici_korisnikID",
                table: "Komentari",
                column: "korisnikID",
                principalTable: "Korisnici",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Komentari_Korisnici_korisnikID",
                table: "Komentari");

            migrationBuilder.DropIndex(
                name: "IX_Komentari_korisnikID",
                table: "Komentari");

            migrationBuilder.DropColumn(
                name: "korisnikID",
                table: "Komentari");
        }
    }
}
