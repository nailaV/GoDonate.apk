using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoDonate.Migrations
{
    /// <inheritdoc />
    public partial class ispravakPricaKorisnik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Price_Kategorije_korisnikID",
                table: "Price");

            migrationBuilder.AddForeignKey(
                name: "FK_Price_Korisnici_korisnikID",
                table: "Price",
                column: "korisnikID",
                principalTable: "Korisnici",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Price_Korisnici_korisnikID",
                table: "Price");

            migrationBuilder.AddForeignKey(
                name: "FK_Price_Kategorije_korisnikID",
                table: "Price",
                column: "korisnikID",
                principalTable: "Kategorije",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
