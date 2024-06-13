using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanThiHoaiAnh_223DATN_DVTC.Migrations
{
    /// <inheritdoc />
    public partial class updatePartyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TableId",
                table: "Party",
                newName: "otherService");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Party",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Party_ServiceId",
                table: "Party",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Party_OtherServices_ServiceId",
                table: "Party",
                column: "ServiceId",
                principalTable: "OtherServices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Party_OtherServices_ServiceId",
                table: "Party");

            migrationBuilder.DropIndex(
                name: "IX_Party_ServiceId",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Party");

            migrationBuilder.RenameColumn(
                name: "otherService",
                table: "Party",
                newName: "TableId");
        }
    }
}
