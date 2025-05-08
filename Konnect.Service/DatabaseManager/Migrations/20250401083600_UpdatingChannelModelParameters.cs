using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Konnekt.Client.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingChannelModelParameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ChannelDescription",
                table: "ServerChannels",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "743da3b6-e3a4-40fb-ae3a-6773b103ee1a",
                column: "ConcurrencyStamp",
                value: "4877b8d8-db49-4f51-9f7f-7d92fb218417");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc3323c1-5f95-4a9b-803f-983c5a6a537e",
                column: "ConcurrencyStamp",
                value: "93696c94-42ce-4d11-bb3a-90b8e17ce0d9");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ChannelDescription",
                table: "ServerChannels",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldNullable: true);

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
        }
    }
}
