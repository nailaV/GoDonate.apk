using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoDonate.Migrations
{
    /// <inheritdoc />
    public partial class godinaImjesecKartica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DatumVazenja",
                table: "Kartice",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "GodinaIsteka",
                table: "Kartice",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MjesecIsteka",
                table: "Kartice",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GodinaIsteka",
                table: "Kartice");

            migrationBuilder.DropColumn(
                name: "MjesecIsteka",
                table: "Kartice");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatumVazenja",
                table: "Kartice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
