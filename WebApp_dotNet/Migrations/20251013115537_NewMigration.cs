using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp_dotNet.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "BidDbs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AuctionDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 13, 13, 55, 36, 711, DateTimeKind.Local).AddTicks(5054));

            migrationBuilder.UpdateData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "Amount", "DateAdded" },
                values: new object[] { 400, new DateTime(2025, 10, 13, 13, 55, 36, 711, DateTimeKind.Local).AddTicks(5277) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "BidDbs");

            migrationBuilder.UpdateData(
                table: "AuctionDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 13, 9, 58, 29, 135, DateTimeKind.Local).AddTicks(8367));

            migrationBuilder.UpdateData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "DateAdded",
                value: new DateTime(2025, 10, 13, 9, 58, 29, 135, DateTimeKind.Local).AddTicks(8603));
        }
    }
}
