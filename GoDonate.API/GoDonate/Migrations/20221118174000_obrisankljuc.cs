using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoDonate.Migrations
{
    /// <inheritdoc />
    public partial class obrisankljuc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Valute_Korisnici_korisnikID",
                table: "Valute");

            migrationBuilder.DropIndex(
                name: "IX_Valute_korisnikID",
                table: "Valute");

            migrationBuilder.DropColumn(
                name: "korisnikID",
                table: "Valute");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "korisnikID",
                table: "Valute",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Valute_korisnikID",
                table: "Valute",
                column: "korisnikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Valute_Korisnici_korisnikID",
                table: "Valute",
                column: "korisnikID",
                principalTable: "Korisnici",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
