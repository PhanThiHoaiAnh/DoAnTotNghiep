using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanThiHoaiAnh_223DATN_DVTC.Migrations
{
    /// <inheritdoc />
    public partial class EditDatTiecModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatTiecs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderOrg = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PartyId = table.Column<int>(type: "int", nullable: false),
                    TableCount = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TiecId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatTiecs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatTiecs_Party_TiecId",
                        column: x => x.TiecId,
                        principalTable: "Party",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatTiecs_TiecId",
                table: "DatTiecs",
                column: "TiecId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatTiecs");
        }
    }
}
