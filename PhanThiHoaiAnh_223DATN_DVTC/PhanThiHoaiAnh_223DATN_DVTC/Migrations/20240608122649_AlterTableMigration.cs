using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanThiHoaiAnh_223DATN_DVTC.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatTiecs_Party_TiecId",
                table: "DatTiecs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DatTiecs",
                table: "DatTiecs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChucVus",
                table: "ChucVus");

            migrationBuilder.RenameTable(
                name: "DatTiecs",
                newName: "OrderParty");

            migrationBuilder.RenameTable(
                name: "ChucVus",
                newName: "Positions");

            migrationBuilder.RenameIndex(
                name: "IX_DatTiecs_TiecId",
                table: "OrderParty",
                newName: "IX_OrderParty_TiecId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderParty",
                table: "OrderParty",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Positions",
                table: "Positions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "WeddingCardCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeddingCardCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderWeddingCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenCoDau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenChuRe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenChaCoDau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenMeCoDau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressCoDau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenChaChuRe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenMeChuRe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressChuRe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeWediing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddrWedding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CardModelId = table.Column<int>(type: "int", nullable: true),
                    NoteMore = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderWeddingCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderWeddingCard_WeddingCardCategories_CardModelId",
                        column: x => x.CardModelId,
                        principalTable: "WeddingCardCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderWeddingCard_CardModelId",
                table: "OrderWeddingCard",
                column: "CardModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderParty_Party_TiecId",
                table: "OrderParty",
                column: "TiecId",
                principalTable: "Party",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderParty_Party_TiecId",
                table: "OrderParty");

            migrationBuilder.DropTable(
                name: "OrderWeddingCard");

            migrationBuilder.DropTable(
                name: "WeddingCardCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Positions",
                table: "Positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderParty",
                table: "OrderParty");

            migrationBuilder.RenameTable(
                name: "Positions",
                newName: "ChucVus");

            migrationBuilder.RenameTable(
                name: "OrderParty",
                newName: "DatTiecs");

            migrationBuilder.RenameIndex(
                name: "IX_OrderParty_TiecId",
                table: "DatTiecs",
                newName: "IX_DatTiecs_TiecId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChucVus",
                table: "ChucVus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DatTiecs",
                table: "DatTiecs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DatTiecs_Party_TiecId",
                table: "DatTiecs",
                column: "TiecId",
                principalTable: "Party",
                principalColumn: "Id");
        }
    }
}
