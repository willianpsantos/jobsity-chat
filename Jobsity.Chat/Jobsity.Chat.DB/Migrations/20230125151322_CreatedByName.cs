using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobsity.Chat.DB.Migrations
{
    /// <inheritdoc />
    public partial class CreatedByName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "ChatRoomParticipant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByName",
                table: "ChatRoomParticipant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "ChatRoom",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByName",
                table: "ChatRoom",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "ChatMessage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByName",
                table: "ChatMessage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "Chat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByName",
                table: "Chat",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "ChatRoomParticipant");

            migrationBuilder.DropColumn(
                name: "UpdatedByName",
                table: "ChatRoomParticipant");

            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "ChatRoom");

            migrationBuilder.DropColumn(
                name: "UpdatedByName",
                table: "ChatRoom");

            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "ChatMessage");

            migrationBuilder.DropColumn(
                name: "UpdatedByName",
                table: "ChatMessage");

            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "UpdatedByName",
                table: "Chat");
        }
    }
}
