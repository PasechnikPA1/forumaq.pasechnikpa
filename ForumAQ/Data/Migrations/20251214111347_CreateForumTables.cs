using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ForumAQ.Migrations
{
    /// <inheritdoc />
    public partial class CreateForumTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86e313df-5cdb-43d3-9377-c53e26bf96cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "895eb6b7-2db4-434b-8854-51ccc5ea5e16");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b185a208-09ea-46ae-af1a-a227453f9e8f", "4c8c7d7a-98ae-4b7c-b67b-19174401ba0c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b185a208-09ea-46ae-af1a-a227453f9e8f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4c8c7d7a-98ae-4b7c-b67b-19174401ba0c");

            migrationBuilder.AddColumn<int>(
                name: "AnswersGiven",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuestionsAsked",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThanksReceived",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    AnswerCount = table.Column<int>(type: "int", nullable: false),
                    IsSolved = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsHelpful = table.Column<bool>(type: "bit", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin", "ADMIN" },
                    { "2", null, "Moderator", "MODERATOR" },
                    { "3", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "AnswerCount", "CreatedDate", "Description", "IsSolved", "Tags", "Title", "UpdatedDate", "UserId", "ViewCount" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2025, 12, 12, 16, 13, 47, 79, DateTimeKind.Local).AddTicks(8718), "Подскажите, с чего начать создание проекта на Blazor Server? Какие шаги нужно выполнить?", false, "blazor c# web", "Как создать проект на Blazor?", new DateTime(2025, 12, 12, 16, 13, 47, 79, DateTimeKind.Local).AddTicks(8722), null, 15 },
                    { 2, 1, new DateTime(2025, 12, 13, 16, 13, 47, 79, DateTimeKind.Local).AddTicks(8724), "Не могу выполнить миграцию, выдает ошибку подключения к базе данных. В чем может быть проблема?", false, "entity-framework sql database", "Проблема с Entity Framework Core", new DateTime(2025, 12, 13, 16, 13, 47, 79, DateTimeKind.Local).AddTicks(8725), null, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserId",
                table: "Answers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_UserId",
                table: "Questions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DropColumn(
                name: "AnswersGiven",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "QuestionsAsked",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ThanksReceived",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "86e313df-5cdb-43d3-9377-c53e26bf96cc", "b48d100e-0e2f-4d46-8784-feeebf6b92d4", "Moderator", "MODERATOR" },
                    { "895eb6b7-2db4-434b-8854-51ccc5ea5e16", "6a8c3afb-cb09-4b3b-b8a8-1eb25b840de1", "User", "USER" },
                    { "b185a208-09ea-46ae-af1a-a227453f9e8f", "101263eb-3de6-4798-8f17-4b0124cd5f03", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Bio", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegistrationDate", "SecurityStamp", "ThanksCount", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4c8c7d7a-98ae-4b7c-b67b-19174401ba0c", 0, "Системный администратор форума", "7411b2d5-8c35-4ce5-a6d3-f6a8458751a5", "Администратор", "admin@forum.com", true, false, null, "ADMIN@FORUM.COM", "ADMIN@FORUM.COM", "AQAAAAIAAYagAAAAEAhrswojD3EaxOqF3Nbh8NmVpEaGulnr8mrWmxPt6TGsl37WjKnYE0VGzaXapRLQhg==", null, false, new DateTime(2025, 12, 14, 13, 12, 4, 849, DateTimeKind.Local).AddTicks(5345), "29e9aff0-b111-4d29-bb4d-964a9ed6d73b", 100, false, "admin@forum.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b185a208-09ea-46ae-af1a-a227453f9e8f", "4c8c7d7a-98ae-4b7c-b67b-19174401ba0c" });
        }
    }
}
