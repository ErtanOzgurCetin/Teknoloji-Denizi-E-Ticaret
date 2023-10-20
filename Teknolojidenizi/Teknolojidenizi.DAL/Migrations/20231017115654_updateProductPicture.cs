using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teknolojidenizi.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateProductPicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastLoginDate",
                value: new DateTime(2023, 10, 17, 14, 56, 54, 239, DateTimeKind.Local).AddTicks(6073));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastLoginDate",
                value: new DateTime(2023, 10, 17, 14, 33, 44, 725, DateTimeKind.Local).AddTicks(5954));
        }
    }
}
