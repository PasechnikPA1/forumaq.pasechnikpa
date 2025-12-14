using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumAQ.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserStatistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 12, 17, 12, 54, 480, DateTimeKind.Local).AddTicks(2724), new DateTime(2025, 12, 12, 17, 12, 54, 480, DateTimeKind.Local).AddTicks(2725) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 13, 17, 12, 54, 480, DateTimeKind.Local).AddTicks(2727), new DateTime(2025, 12, 13, 17, 12, 54, 480, DateTimeKind.Local).AddTicks(2728) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 57, 48, 435, DateTimeKind.Local).AddTicks(5787), new DateTime(2025, 12, 12, 16, 57, 48, 435, DateTimeKind.Local).AddTicks(5794) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 13, 16, 57, 48, 435, DateTimeKind.Local).AddTicks(5797), new DateTime(2025, 12, 13, 16, 57, 48, 435, DateTimeKind.Local).AddTicks(5797) });
        }
    }
}
