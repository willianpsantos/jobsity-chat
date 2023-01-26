using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobsity.Chat.DB.Migrations
{
    /// <inheritdoc />
    public partial class UserFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "ChatRoomParticipant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ChatRoomParticipant",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "ChatRoomParticipant");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ChatRoomParticipant");
        }
    }
}
