using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanThiHoaiAnh_223DATN_DVTC.Migrations
{
    /// <inheritdoc />
    public partial class updateMemberMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VịTriCV",
                table: "Members",
                newName: "Rolee");

            migrationBuilder.AddColumn<string>(
                name: "IRoleId",
                table: "Members",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_IRoleId",
                table: "Members",
                column: "IRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_AspNetRoles_IRoleId",
                table: "Members",
                column: "IRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_AspNetRoles_IRoleId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_IRoleId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "IRoleId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "Rolee",
                table: "Members",
                newName: "VịTriCV");
        }
    }
}
