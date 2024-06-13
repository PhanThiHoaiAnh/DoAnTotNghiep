using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanThiHoaiAnh_223DATN_DVTC.Migrations
{
    /// <inheritdoc />
    public partial class UpdatetblPartyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Deposit",
                table: "Party",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Pay",
                table: "Party",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deposit",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "Pay",
                table: "Party");
        }
    }
}
