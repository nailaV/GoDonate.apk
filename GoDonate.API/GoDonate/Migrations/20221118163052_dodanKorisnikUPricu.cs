using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoDonate.Migrations
{
    /// <inheritdoc />
    public partial class dodanKorisnikUPricu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "korisnikID",
                table: "Price",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Price_korisnikID",
                table: "Price",
                column: "korisnikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Price_Kategorije_korisnikID",
                table: "Price",
                column: "korisnikID",
                principalTable: "Kategorije",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Price_Kategorije_korisnikID",
                table: "Price");

            migrationBuilder.DropIndex(
                name: "IX_Price_korisnikID",
                table: "Price");

            migrationBuilder.DropColumn(
                name: "korisnikID",
                table: "Price");
        }
    }
}
