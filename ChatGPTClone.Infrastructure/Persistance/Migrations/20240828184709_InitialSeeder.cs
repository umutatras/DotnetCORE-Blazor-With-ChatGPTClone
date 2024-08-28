using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatGPTClone.Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessage");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedByUserId", "CreatedOn", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedByUserId", "ModifiedOn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("2798212b-3e5d-4556-8629-a64eb70da4a8"), 0, "e4ff6959-7ef4-4d26-89f0-81b9fe15d9f4", "2798212b-3e5d-4556-8629-a64eb70da4a8", new DateTimeOffset(new DateTime(2024, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "umut@gmail.com", true, "Umut", "Atraş", false, null, null, null, "UMUT@GMAIL.COM", "UMUT", "AQAAAAIAAYagAAAAEP4Bj7bF6wkzUH79Yy4M0u9ffnW1PQbRmjGVF+DBORiogH5H7e9Ct0dHhJs+RVlquQ==", null, false, "6a17d88e-ac30-43c4-9442-4de3cad7aeaa", false, "umut" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2798212b-3e5d-4556-8629-a64eb70da4a8"));

            migrationBuilder.CreateTable(
                name: "ChatMessage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ChatSessionId = table.Column<Guid>(type: "uuid", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Model = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessage_ChatSessions_ChatSessionId",
                        column: x => x.ChatSessionId,
                        principalTable: "ChatSessions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_ChatSessionId",
                table: "ChatMessage",
                column: "ChatSessionId");
        }
    }
}
