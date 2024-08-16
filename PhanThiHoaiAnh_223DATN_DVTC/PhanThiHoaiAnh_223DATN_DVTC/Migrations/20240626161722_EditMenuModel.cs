using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanThiHoaiAnh_223DATN_DVTC.Migrations
{
    /// <inheritdoc />
    public partial class EditMenuModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "tblMenu");

            migrationBuilder.AddColumn<int>(
                name: "MenuModelId",
                table: "tblFood",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Selected",
                table: "tblFood",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_tblFood_MenuModelId",
                table: "tblFood",
                column: "MenuModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblFood_tblMenu_MenuModelId",
                table: "tblFood",
                column: "MenuModelId",
                principalTable: "tblMenu",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblFood_tblMenu_MenuModelId",
                table: "tblFood");

            migrationBuilder.DropIndex(
                name: "IX_tblFood_MenuModelId",
                table: "tblFood");

            migrationBuilder.DropColumn(
                name: "MenuModelId",
                table: "tblFood");

            migrationBuilder.DropColumn(
                name: "Selected",
                table: "tblFood");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "tblMenu",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
