using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobsity.Chat.DB.Migrations
{
    /// <inheritdoc />
    public partial class ChatAdjusts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceiverEmail",
                table: "Chat",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReceiverId",
                table: "Chat",
                type: "uniqueidentifier",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                table: "Chat",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceiverType",
                table: "Chat",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SenderUserId",
                table: "Chat",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverEmail",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "ReceiverName",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "ReceiverType",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "SenderUserId",
                table: "Chat");
        }
    }
}
