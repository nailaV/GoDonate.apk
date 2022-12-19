using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoDonate.Migrations
{
    /// <inheritdoc />
    public partial class korsnik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "SlikaKorisnika",
                table: "KorisnickiNalozi",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlikaKorisnika",
                table: "KorisnickiNalozi");
        }
    }
}
