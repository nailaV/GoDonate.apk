using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoDonate.Migrations
{
    /// <inheritdoc />
    public partial class noviPodaci : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrojTelefona",
                table: "KorisnickiNalozi",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "KorisnickiNalozi",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrojTelefona",
                table: "KorisnickiNalozi");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "KorisnickiNalozi");
        }
    }
}
