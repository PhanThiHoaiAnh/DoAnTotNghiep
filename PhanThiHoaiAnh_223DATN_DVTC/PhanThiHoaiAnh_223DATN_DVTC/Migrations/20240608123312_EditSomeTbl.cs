using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanThiHoaiAnh_223DATN_DVTC.Migrations
{
    /// <inheritdoc />
    public partial class EditSomeTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDons_UserModel_UserId",
                table: "HoaDons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HoaDons",
                table: "HoaDons");

            migrationBuilder.RenameTable(
                name: "HoaDons",
                newName: "Bills");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDons_UserId",
                table: "Bills",
                newName: "IX_Bills_UserId");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Party",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bills",
                table: "Bills",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_UserModel_UserId",
                table: "Bills",
                column: "UserId",
                principalTable: "UserModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_UserModel_UserId",
                table: "Bills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bills",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Party");

            migrationBuilder.RenameTable(
                name: "Bills",
                newName: "HoaDons");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_UserId",
                table: "HoaDons",
                newName: "IX_HoaDons_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HoaDons",
                table: "HoaDons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDons_UserModel_UserId",
                table: "HoaDons",
                column: "UserId",
                principalTable: "UserModel",
                principalColumn: "Id");
        }
    }
}
