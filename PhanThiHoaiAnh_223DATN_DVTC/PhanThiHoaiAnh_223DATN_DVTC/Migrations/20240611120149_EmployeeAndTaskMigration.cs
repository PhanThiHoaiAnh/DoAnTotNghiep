using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanThiHoaiAnh_223DATN_DVTC.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeAndTaskMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VịTriCV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChucVuId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_Positions_ChucVuId",
                        column: x => x.ChucVuId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tblTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idNhanVien = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Party = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NhanVienId = table.Column<int>(type: "int", nullable: true),
                    TiecId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblTasks_Members_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "Members",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblTasks_Party_TiecId",
                        column: x => x.TiecId,
                        principalTable: "Party",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_ChucVuId",
                table: "Members",
                column: "ChucVuId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTasks_NhanVienId",
                table: "tblTasks",
                column: "NhanVienId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTasks_TiecId",
                table: "tblTasks",
                column: "TiecId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblTasks");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
