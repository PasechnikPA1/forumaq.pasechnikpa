using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumAQ.Migrations
{
    /// <inheritdoc />
    public partial class AddThanksSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThanksCount",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ThanksHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ThankedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanksHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThanksHistories_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThanksHistories_AspNetUsers_UserId",
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
                values: new object[] { new DateTime(2025, 12, 12, 16, 57, 48, 435, DateTimeKind.Local).AddTicks(5787), new DateTime(2025, 12, 12, 16, 57, 48, 435, DateTimeKind.Local).AddTicks(5794) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 13, 16, 57, 48, 435, DateTimeKind.Local).AddTicks(5797), new DateTime(2025, 12, 13, 16, 57, 48, 435, DateTimeKind.Local).AddTicks(5797) });

            migrationBuilder.CreateIndex(
                name: "IX_ThanksHistories_AnswerId_UserId",
                table: "ThanksHistories",
                columns: new[] { "AnswerId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThanksHistories_UserId",
                table: "ThanksHistories",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThanksHistories");

            migrationBuilder.DropColumn(
                name: "ThanksCount",
                table: "Answers");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 12, 16, 13, 47, 79, DateTimeKind.Local).AddTicks(8718), new DateTime(2025, 12, 12, 16, 13, 47, 79, DateTimeKind.Local).AddTicks(8722) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 13, 16, 13, 47, 79, DateTimeKind.Local).AddTicks(8724), new DateTime(2025, 12, 13, 16, 13, 47, 79, DateTimeKind.Local).AddTicks(8725) });
        }
    }
}
