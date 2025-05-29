using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Konnekt.Client.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingServerMessageModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServerMessages");

            migrationBuilder.CreateTable(
                name: "ServerMessage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR(36)", nullable: false),
                    ChannelId = table.Column<string>(type: "NVARCHAR(36)", nullable: false),
                    MessageBody = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerMessage_ServerChannels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "ServerChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "743da3b6-e3a4-40fb-ae3a-6773b103ee1a",
                column: "ConcurrencyStamp",
                value: "4803c05a-f6df-4fb2-8588-9b17aa9085ec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc3323c1-5f95-4a9b-803f-983c5a6a537e",
                column: "ConcurrencyStamp",
                value: "f0eb4866-f8e5-42e6-9de6-25af72d21ed0");

            migrationBuilder.CreateIndex(
                name: "IX_ServerMessage_ChannelId",
                table: "ServerMessage",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerMessage_Id",
                table: "ServerMessage",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServerMessage");

            migrationBuilder.CreateTable(
                name: "ServerMessages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR(36)", nullable: false),
                    ChannelId = table.Column<string>(type: "NVARCHAR(36)", nullable: false),
                    MessageBody = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerMessages_ServerChannels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "ServerChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "743da3b6-e3a4-40fb-ae3a-6773b103ee1a",
                column: "ConcurrencyStamp",
                value: "a94197f4-2722-474e-80bd-3582254240ee");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc3323c1-5f95-4a9b-803f-983c5a6a537e",
                column: "ConcurrencyStamp",
                value: "3792c9c2-2d51-40ca-b702-39e538769585");

            migrationBuilder.CreateIndex(
                name: "IX_ServerMessages_ChannelId",
                table: "ServerMessages",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerMessages_Id",
                table: "ServerMessages",
                column: "Id",
                unique: true);
        }
    }
}
