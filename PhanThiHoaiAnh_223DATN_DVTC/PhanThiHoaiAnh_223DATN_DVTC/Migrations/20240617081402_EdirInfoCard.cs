using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhanThiHoaiAnh_223DATN_DVTC.Migrations
{
    /// <inheritdoc />
    public partial class EdirInfoCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddrWedding",
                table: "tblOrderWeddingCard");

            migrationBuilder.RenameColumn(
                name: "TimeWediing",
                table: "tblOrderWeddingCard",
                newName: "TimeWedding");

            migrationBuilder.RenameColumn(
                name: "TenMeCoDau",
                table: "tblOrderWeddingCard",
                newName: "GroomName");

            migrationBuilder.RenameColumn(
                name: "TenMeChuRe",
                table: "tblOrderWeddingCard",
                newName: "GroomMoName");

            migrationBuilder.RenameColumn(
                name: "TenCoDau",
                table: "tblOrderWeddingCard",
                newName: "GroomFaName");

            migrationBuilder.RenameColumn(
                name: "TenChuRe",
                table: "tblOrderWeddingCard",
                newName: "GroomAddress");

            migrationBuilder.RenameColumn(
                name: "TenChaCoDau",
                table: "tblOrderWeddingCard",
                newName: "BrideName");

            migrationBuilder.RenameColumn(
                name: "TenChaChuRe",
                table: "tblOrderWeddingCard",
                newName: "BrideMoName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tblOrderWeddingCard",
                newName: "BrideFaName");

            migrationBuilder.RenameColumn(
                name: "AddressCoDau",
                table: "tblOrderWeddingCard",
                newName: "BrideAddress");

            migrationBuilder.RenameColumn(
                name: "AddressChuRe",
                table: "tblOrderWeddingCard",
                newName: "AddressWedding");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "tblOrderWeddingCard",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateWedding",
                table: "tblOrderWeddingCard",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "tblOrderWeddingCard");

            migrationBuilder.DropColumn(
                name: "DateWedding",
                table: "tblOrderWeddingCard");

            migrationBuilder.RenameColumn(
                name: "TimeWedding",
                table: "tblOrderWeddingCard",
                newName: "TimeWediing");

            migrationBuilder.RenameColumn(
                name: "GroomName",
                table: "tblOrderWeddingCard",
                newName: "TenMeCoDau");

            migrationBuilder.RenameColumn(
                name: "GroomMoName",
                table: "tblOrderWeddingCard",
                newName: "TenMeChuRe");

            migrationBuilder.RenameColumn(
                name: "GroomFaName",
                table: "tblOrderWeddingCard",
                newName: "TenCoDau");

            migrationBuilder.RenameColumn(
                name: "GroomAddress",
                table: "tblOrderWeddingCard",
                newName: "TenChuRe");

            migrationBuilder.RenameColumn(
                name: "BrideName",
                table: "tblOrderWeddingCard",
                newName: "TenChaCoDau");

            migrationBuilder.RenameColumn(
                name: "BrideMoName",
                table: "tblOrderWeddingCard",
                newName: "TenChaChuRe");

            migrationBuilder.RenameColumn(
                name: "BrideFaName",
                table: "tblOrderWeddingCard",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "BrideAddress",
                table: "tblOrderWeddingCard",
                newName: "AddressCoDau");

            migrationBuilder.RenameColumn(
                name: "AddressWedding",
                table: "tblOrderWeddingCard",
                newName: "AddressChuRe");

            migrationBuilder.AddColumn<string>(
                name: "AddrWedding",
                table: "tblOrderWeddingCard",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
