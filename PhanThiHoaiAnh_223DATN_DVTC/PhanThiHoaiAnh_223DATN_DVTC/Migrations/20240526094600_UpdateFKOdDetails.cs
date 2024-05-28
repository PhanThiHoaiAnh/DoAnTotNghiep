using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanThiHoaiAnh_223DATN_DVTC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFKOdDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceId1",
                table: "OrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ServiceId1",
                table: "OrderDetails",
                column: "ServiceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_OtherServices_ServiceId1",
                table: "OrderDetails",
                column: "ServiceId1",
                principalTable: "OtherServices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_OtherServices_ServiceId1",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ServiceId1",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ServiceId1",
                table: "OrderDetails");
        }
    }
}
