using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanThiHoaiAnh_223DATN_DVTC.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableDatabaseMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Party_TableCategories_TablePtId",
                table: "Party");

            migrationBuilder.DropTable(
                name: "TableCategories");

            migrationBuilder.DropIndex(
                name: "IX_Party_TablePtId",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "TablePtId",
                table: "Party");

            migrationBuilder.RenameColumn(
                name: "NoteMore",
                table: "OrderWeddingCard",
                newName: "Note");

            migrationBuilder.AddColumn<int>(
                name: "NumTable",
                table: "Party",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "OrderWeddingCard",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "OrderWeddingCard",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "OrderWeddingCard",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PartyId",
                table: "Bills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderWeddingCard_UserId",
                table: "OrderWeddingCard",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PartyId",
                table: "Bills",
                column: "PartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Party_PartyId",
                table: "Bills",
                column: "PartyId",
                principalTable: "Party",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserModel_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "UserModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderWeddingCard_UserModel_UserId",
                table: "OrderWeddingCard",
                column: "UserId",
                principalTable: "UserModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Party_PartyId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserModel_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderWeddingCard_UserModel_UserId",
                table: "OrderWeddingCard");

            migrationBuilder.DropIndex(
                name: "IX_OrderWeddingCard_UserId",
                table: "OrderWeddingCard");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Bills_PartyId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "NumTable",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "OrderWeddingCard");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderWeddingCard");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "OrderWeddingCard");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "OrderWeddingCard",
                newName: "NoteMore");

            migrationBuilder.AddColumn<int>(
                name: "TablePtId",
                table: "Party",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TableCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Party_TablePtId",
                table: "Party",
                column: "TablePtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Party_TableCategories_TablePtId",
                table: "Party",
                column: "TablePtId",
                principalTable: "TableCategories",
                principalColumn: "Id");
        }
    }
}
