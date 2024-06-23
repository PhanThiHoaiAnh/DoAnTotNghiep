using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanThiHoaiAnh_223DATN_DVTC.Migrations
{
    /// <inheritdoc />
    public partial class AlterRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblParty_tblOtherServices_ServiceId",
                table: "tblParty");

            migrationBuilder.DropIndex(
                name: "IX_tblParty_ServiceId",
                table: "tblParty");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "tblParty");

            migrationBuilder.AddColumn<int>(
                name: "DetailId",
                table: "tblOrder",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblOrder_DetailId",
                table: "tblOrder",
                column: "DetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblOrder_tblOrderDetails_DetailId",
                table: "tblOrder",
                column: "DetailId",
                principalTable: "tblOrderDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblOrder_tblOrderDetails_DetailId",
                table: "tblOrder");

            migrationBuilder.DropIndex(
                name: "IX_tblOrder_DetailId",
                table: "tblOrder");

            migrationBuilder.DropColumn(
                name: "DetailId",
                table: "tblOrder");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "tblParty",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblParty_ServiceId",
                table: "tblParty",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblParty_tblOtherServices_ServiceId",
                table: "tblParty",
                column: "ServiceId",
                principalTable: "tblOtherServices",
                principalColumn: "Id");
        }
    }
}
