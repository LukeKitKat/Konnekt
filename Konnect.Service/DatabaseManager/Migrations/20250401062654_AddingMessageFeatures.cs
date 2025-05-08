using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Konnekt.Client.Migrations
{
    /// <inheritdoc />
    public partial class AddingMessageFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "JoinCode",
                table: "ServerJoinCodes",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ServerChannels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR(36)", nullable: false),
                    ServerId = table.Column<string>(type: "NVARCHAR(36)", nullable: false),
                    ChannelName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ChannelDescription = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    ChannelOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerChannels_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                value: "378c0fa9-f980-4046-98fa-70a58bd5aa33");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc3323c1-5f95-4a9b-803f-983c5a6a537e",
                column: "ConcurrencyStamp",
                value: "2fe8f78d-ccb5-4da0-b47e-95295cefbb72");

            migrationBuilder.CreateIndex(
                name: "IX_ServerChannels_Id",
                table: "ServerChannels",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServerChannels_ServerId",
                table: "ServerChannels",
                column: "ServerId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServerMessages");

            migrationBuilder.DropTable(
                name: "ServerChannels");

            migrationBuilder.AlterColumn<string>(
                name: "JoinCode",
                table: "ServerJoinCodes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "743da3b6-e3a4-40fb-ae3a-6773b103ee1a",
                column: "ConcurrencyStamp",
                value: "5ff2b2af-2395-4a23-b8f2-e00bb1aa2dd2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc3323c1-5f95-4a9b-803f-983c5a6a537e",
                column: "ConcurrencyStamp",
                value: "459db496-bf72-4630-b87e-60e24ee25dba");
        }
    }
}
