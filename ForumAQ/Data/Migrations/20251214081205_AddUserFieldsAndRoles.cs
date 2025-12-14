using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ForumAQ.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFieldsAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ThanksCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ThanksCount",
                table: "AspNetUsers");
        }
    }
}
