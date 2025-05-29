using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Konnekt.Client.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingMessageRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "ServerMessage",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "743da3b6-e3a4-40fb-ae3a-6773b103ee1a",
                column: "ConcurrencyStamp",
                value: "6162d6cc-20a6-4576-9f0b-4a3a774cd724");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc3323c1-5f95-4a9b-803f-983c5a6a537e",
                column: "ConcurrencyStamp",
                value: "9e303d31-e8a4-43a0-8dd3-4238d18d7bf2");

            migrationBuilder.CreateIndex(
                name: "IX_ServerMessage_SenderId",
                table: "ServerMessage",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServerMessage_AspNetUsers_SenderId",
                table: "ServerMessage",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServerMessage_AspNetUsers_SenderId",
                table: "ServerMessage");

            migrationBuilder.DropIndex(
                name: "IX_ServerMessage_SenderId",
                table: "ServerMessage");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "ServerMessage");

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
        }
    }
}
