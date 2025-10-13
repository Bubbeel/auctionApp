using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp_dotNet.Migrations
{
    /// <inheritdoc />
    public partial class FixesToAuction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentPrice",
                table: "AuctionDbs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AuctionDbs",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "CurrentPrice" },
                values: new object[] { new DateTime(2025, 10, 13, 9, 58, 29, 135, DateTimeKind.Local).AddTicks(8367), 300 });

            migrationBuilder.UpdateData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "DateAdded",
                value: new DateTime(2025, 10, 13, 9, 58, 29, 135, DateTimeKind.Local).AddTicks(8603));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "AuctionDbs");

            migrationBuilder.UpdateData(
                table: "AuctionDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 12, 13, 58, 12, 242, DateTimeKind.Local).AddTicks(8954));

            migrationBuilder.UpdateData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "DateAdded",
                value: new DateTime(2025, 10, 12, 13, 58, 12, 242, DateTimeKind.Local).AddTicks(9147));
        }
    }
}
