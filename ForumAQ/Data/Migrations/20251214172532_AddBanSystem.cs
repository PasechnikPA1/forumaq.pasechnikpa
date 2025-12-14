using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumAQ.Migrations
{
    /// <inheritdoc />
    public partial class AddBanSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BanRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModeratorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BannedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BannedUntil = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BanType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BanRecords_AspNetUsers_ModeratorId",
                        column: x => x.ModeratorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BanRecords_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 12, 22, 25, 32, 162, DateTimeKind.Local).AddTicks(8776), new DateTime(2025, 12, 12, 22, 25, 32, 162, DateTimeKind.Local).AddTicks(8779) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 13, 22, 25, 32, 162, DateTimeKind.Local).AddTicks(8781), new DateTime(2025, 12, 13, 22, 25, 32, 162, DateTimeKind.Local).AddTicks(8781) });

            migrationBuilder.CreateIndex(
                name: "IX_BanRecords_BannedAt",
                table: "BanRecords",
                column: "BannedAt");

            migrationBuilder.CreateIndex(
                name: "IX_BanRecords_BannedUntil",
                table: "BanRecords",
                column: "BannedUntil");

            migrationBuilder.CreateIndex(
                name: "IX_BanRecords_ModeratorId",
                table: "BanRecords",
                column: "ModeratorId");

            migrationBuilder.CreateIndex(
                name: "IX_BanRecords_UserId",
                table: "BanRecords",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BanRecords");

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
    }
}
