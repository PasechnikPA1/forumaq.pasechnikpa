using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumAQ.Migrations
{
    /// <inheritdoc />
    public partial class AddUserUpdatedDateAndProfileFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 12, 18, 24, 48, 789, DateTimeKind.Local).AddTicks(1201), new DateTime(2025, 12, 12, 18, 24, 48, 789, DateTimeKind.Local).AddTicks(1203) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 13, 18, 24, 48, 789, DateTimeKind.Local).AddTicks(1205), new DateTime(2025, 12, 13, 18, 24, 48, 789, DateTimeKind.Local).AddTicks(1206) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "AspNetUsers");

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
    }
}
