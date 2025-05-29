using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Konnekt.Client.Migrations
{
    /// <inheritdoc />
    public partial class AddingMessageContentFramework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServerMessageFile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR(36)", nullable: false),
                    ServerMessageId = table.Column<string>(type: "NVARCHAR(36)", nullable: false),
                    FileContent = table.Column<byte[]>(type: "varbinary(MAX)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerMessageFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerMessageFile_ServerMessage_ServerMessageId",
                        column: x => x.ServerMessageId,
                        principalTable: "ServerMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "743da3b6-e3a4-40fb-ae3a-6773b103ee1a",
                column: "ConcurrencyStamp",
                value: "4be35313-e649-43a8-9438-9d44888b87b1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc3323c1-5f95-4a9b-803f-983c5a6a537e",
                column: "ConcurrencyStamp",
                value: "776ac07d-ecc9-4f82-9c83-1a7b8280a40a");

            migrationBuilder.CreateIndex(
                name: "IX_ServerMessageFile_Id",
                table: "ServerMessageFile",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServerMessageFile_ServerMessageId",
                table: "ServerMessageFile",
                column: "ServerMessageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServerMessageFile");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "743da3b6-e3a4-40fb-ae3a-6773b103ee1a",
                column: "ConcurrencyStamp",
                value: "c80cb1f8-1a82-48c9-87e2-f4962e798e1e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc3323c1-5f95-4a9b-803f-983c5a6a537e",
                column: "ConcurrencyStamp",
                value: "a8a133b6-9e4d-4c79-80ec-c42741ff7f13");
        }
    }
}
