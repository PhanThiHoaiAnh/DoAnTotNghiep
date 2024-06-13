using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanThiHoaiAnh_223DATN_DVTC.Migrations
{
    /// <inheritdoc />
    public partial class EditDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deposit",
                table: "DatTiecs");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "DatTiecs");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "DatTiecs");

            migrationBuilder.AddColumn<string>(
                name: "PartyCode",
                table: "Party",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonTable",
                table: "Party",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MenuName",
                table: "DatTiecs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartyCode",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "PersonTable",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "MenuName",
                table: "DatTiecs");

            migrationBuilder.AddColumn<decimal>(
                name: "Deposit",
                table: "DatTiecs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "DatTiecs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartyId",
                table: "DatTiecs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
