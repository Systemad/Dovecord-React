using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TextChannels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextChannels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Online = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChannelMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsEdit = table.Column<bool>(type: "INTEGER", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TextChannelId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChannelMessages_TextChannels_TextChannelId",
                        column: x => x.TextChannelId,
                        principalTable: "TextChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelMessages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TextChannels",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("9c73037b-5d64-4c56-bf8f-1dea5c4aadf8"), "Random" });

            migrationBuilder.InsertData(
                table: "TextChannels",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("f81cf04e-d204-4493-aaa9-76121ab95291"), "General" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Online", "UserId", "Username" },
                values: new object[] { new Guid("ca0f4479-5992-4a00-a3d5-d73ae1daff6f"), false, null, "danova" });

            migrationBuilder.InsertData(
                table: "ChannelMessages",
                columns: new[] { "Id", "Content", "CreatedAt", "IsEdit", "TextChannelId", "UserId", "Username" },
                values: new object[] { new Guid("72ff6abf-ebc0-4f7c-85d5-0ede201da17b"), "First ever channel message", new DateTime(2021, 11, 17, 1, 38, 34, 373, DateTimeKind.Local).AddTicks(4950), false, new Guid("f81cf04e-d204-4493-aaa9-76121ab95291"), new Guid("ca0f4479-5992-4a00-a3d5-d73ae1daff6f"), "danova" });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelMessages_TextChannelId",
                table: "ChannelMessages",
                column: "TextChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelMessages_UserId",
                table: "ChannelMessages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId",
                table: "Users",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelMessages");

            migrationBuilder.DropTable(
                name: "TextChannels");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
