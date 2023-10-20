using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teknolojidenizi.DAL.Migrations
{
    /// <inheritdoc />
    public partial class test11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastLoginDate",
                value: new DateTime(2023, 10, 20, 3, 39, 51, 382, DateTimeKind.Local).AddTicks(5096));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastLoginDate",
                value: new DateTime(2023, 10, 20, 3, 33, 14, 785, DateTimeKind.Local).AddTicks(823));
        }
    }
}
