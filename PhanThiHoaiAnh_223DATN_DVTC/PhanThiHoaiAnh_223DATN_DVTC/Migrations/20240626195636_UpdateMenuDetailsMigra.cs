using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanThiHoaiAnh_223DATN_DVTC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMenuDetailsMigra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "tblMenuDetails");

            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "tblMenuDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "tblMenuDetails");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "tblMenuDetails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
