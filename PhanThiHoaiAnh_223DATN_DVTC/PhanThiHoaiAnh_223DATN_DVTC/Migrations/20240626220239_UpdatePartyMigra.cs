using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanThiHoaiAnh_223DATN_DVTC.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePartyMigra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "otherService",
                table: "tblParty");

            migrationBuilder.AddColumn<string>(
                name: "FoodName",
                table: "tblParty",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelectedServiceItems",
                table: "tblParty",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "otherServiceName",
                table: "tblParty",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartyModelId",
                table: "tblOtherServices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblOtherServices_PartyModelId",
                table: "tblOtherServices",
                column: "PartyModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblOtherServices_tblParty_PartyModelId",
                table: "tblOtherServices",
                column: "PartyModelId",
                principalTable: "tblParty",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblOtherServices_tblParty_PartyModelId",
                table: "tblOtherServices");

            migrationBuilder.DropIndex(
                name: "IX_tblOtherServices_PartyModelId",
                table: "tblOtherServices");

            migrationBuilder.DropColumn(
                name: "FoodName",
                table: "tblParty");

            migrationBuilder.DropColumn(
                name: "SelectedServiceItems",
                table: "tblParty");

            migrationBuilder.DropColumn(
                name: "otherServiceName",
                table: "tblParty");

            migrationBuilder.DropColumn(
                name: "PartyModelId",
                table: "tblOtherServices");

            migrationBuilder.AddColumn<int>(
                name: "otherService",
                table: "tblParty",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
