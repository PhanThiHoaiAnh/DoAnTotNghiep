using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanThiHoaiAnh_223DATN_DVTC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderPartyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MenuName",
                table: "OrderParty",
                newName: "ServiceName");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "OrderParty",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderParty");

            migrationBuilder.RenameColumn(
                name: "ServiceName",
                table: "OrderParty",
                newName: "MenuName");
        }
    }
}
