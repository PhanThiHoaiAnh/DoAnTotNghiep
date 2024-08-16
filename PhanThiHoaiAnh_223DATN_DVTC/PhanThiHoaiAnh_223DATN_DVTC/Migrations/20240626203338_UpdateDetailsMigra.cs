using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanThiHoaiAnh_223DATN_DVTC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDetailsMigra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "tblOrder",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblOrder_MenuId",
                table: "tblOrder",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblOrder_tblMenu_MenuId",
                table: "tblOrder",
                column: "MenuId",
                principalTable: "tblMenu",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblOrder_tblMenu_MenuId",
                table: "tblOrder");

            migrationBuilder.DropIndex(
                name: "IX_tblOrder_MenuId",
                table: "tblOrder");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "tblOrder");
        }
    }
}
