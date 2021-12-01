using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class FixKeyname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChannelMessages_Users_UserForeginKey",
                table: "ChannelMessages");

            migrationBuilder.RenameColumn(
                name: "UserForeginKey",
                table: "ChannelMessages",
                newName: "UserForeignKey");

            migrationBuilder.RenameIndex(
                name: "IX_ChannelMessages_UserForeginKey",
                table: "ChannelMessages",
                newName: "IX_ChannelMessages_UserForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelMessages_Users_UserForeignKey",
                table: "ChannelMessages",
                column: "UserForeignKey",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChannelMessages_Users_UserForeignKey",
                table: "ChannelMessages");

            migrationBuilder.RenameColumn(
                name: "UserForeignKey",
                table: "ChannelMessages",
                newName: "UserForeginKey");

            migrationBuilder.RenameIndex(
                name: "IX_ChannelMessages_UserForeignKey",
                table: "ChannelMessages",
                newName: "IX_ChannelMessages_UserForeginKey");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelMessages_Users_UserForeginKey",
                table: "ChannelMessages",
                column: "UserForeginKey",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
