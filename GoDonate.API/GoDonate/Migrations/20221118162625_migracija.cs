using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoDonate.Migrations
{
    /// <inheritdoc />
    public partial class migracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jezici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    korisnikID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jezici", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jezici_Korisnici_korisnikID",
                        column: x => x.korisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kartice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojKartice = table.Column<int>(type: "int", nullable: false),
                    TipKartice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CVVCVC = table.Column<int>(name: "CVV_CVC", type: "int", nullable: false),
                    DatumVazenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KorisnikID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kartice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kartice_Korisnici_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Komentari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pricaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komentari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Komentari_Price_pricaID",
                        column: x => x.pricaID,
                        principalTable: "Price",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Obavijesti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipObavijesti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumObavjestenja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavijesti", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Poruke",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    korisnikID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poruke", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Poruke_Korisnici_korisnikID",
                        column: x => x.korisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Valute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skracenica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    korisnikID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Valute_Korisnici_korisnikID",
                        column: x => x.korisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Donacije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KolicinaNovca = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    karticaID = table.Column<int>(type: "int", nullable: false),
                    pricaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donacije", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Donacije_Kartice_karticaID",
                        column: x => x.karticaID,
                        principalTable: "Kartice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Donacije_Price_pricaID",
                        column: x => x.pricaID,
                        principalTable: "Price",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donacije_karticaID",
                table: "Donacije",
                column: "karticaID");

            migrationBuilder.CreateIndex(
                name: "IX_Donacije_pricaID",
                table: "Donacije",
                column: "pricaID");

            migrationBuilder.CreateIndex(
                name: "IX_Jezici_korisnikID",
                table: "Jezici",
                column: "korisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Kartice_KorisnikID",
                table: "Kartice",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Komentari_pricaID",
                table: "Komentari",
                column: "pricaID");

            migrationBuilder.CreateIndex(
                name: "IX_Poruke_korisnikID",
                table: "Poruke",
                column: "korisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Valute_korisnikID",
                table: "Valute",
                column: "korisnikID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donacije");

            migrationBuilder.DropTable(
                name: "Jezici");

            migrationBuilder.DropTable(
                name: "Komentari");

            migrationBuilder.DropTable(
                name: "Obavijesti");

            migrationBuilder.DropTable(
                name: "Poruke");

            migrationBuilder.DropTable(
                name: "Valute");

            migrationBuilder.DropTable(
                name: "Kartice");
        }
    }
}
