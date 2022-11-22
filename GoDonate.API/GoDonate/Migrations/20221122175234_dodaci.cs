using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoDonate.Migrations
{
    /// <inheritdoc />
    public partial class dodaci : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "korisnikID",
                table: "Obavijesti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "valutaID",
                table: "Korisnici",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "valutaID",
                table: "Drzave",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "korisnikID",
                table: "Donacije",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Obavijesti_korisnikID",
                table: "Obavijesti",
                column: "korisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_valutaID",
                table: "Korisnici",
                column: "valutaID");

            migrationBuilder.CreateIndex(
                name: "IX_Drzave_valutaID",
                table: "Drzave",
                column: "valutaID");

            migrationBuilder.CreateIndex(
                name: "IX_Donacije_korisnikID",
                table: "Donacije",
                column: "korisnikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Donacije_Korisnici_korisnikID",
                table: "Donacije",
                column: "korisnikID",
                principalTable: "Korisnici",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Drzave_Valute_valutaID",
                table: "Drzave",
                column: "valutaID",
                principalTable: "Valute",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnici_Valute_valutaID",
                table: "Korisnici",
                column: "valutaID",
                principalTable: "Valute",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Obavijesti_Korisnici_korisnikID",
                table: "Obavijesti",
                column: "korisnikID",
                principalTable: "Korisnici",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donacije_Korisnici_korisnikID",
                table: "Donacije");

            migrationBuilder.DropForeignKey(
                name: "FK_Drzave_Valute_valutaID",
                table: "Drzave");

            migrationBuilder.DropForeignKey(
                name: "FK_Korisnici_Valute_valutaID",
                table: "Korisnici");

            migrationBuilder.DropForeignKey(
                name: "FK_Obavijesti_Korisnici_korisnikID",
                table: "Obavijesti");

            migrationBuilder.DropIndex(
                name: "IX_Obavijesti_korisnikID",
                table: "Obavijesti");

            migrationBuilder.DropIndex(
                name: "IX_Korisnici_valutaID",
                table: "Korisnici");

            migrationBuilder.DropIndex(
                name: "IX_Drzave_valutaID",
                table: "Drzave");

            migrationBuilder.DropIndex(
                name: "IX_Donacije_korisnikID",
                table: "Donacije");

            migrationBuilder.DropColumn(
                name: "korisnikID",
                table: "Obavijesti");

            migrationBuilder.DropColumn(
                name: "valutaID",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "valutaID",
                table: "Drzave");

            migrationBuilder.DropColumn(
                name: "korisnikID",
                table: "Donacije");
        }
    }
}
