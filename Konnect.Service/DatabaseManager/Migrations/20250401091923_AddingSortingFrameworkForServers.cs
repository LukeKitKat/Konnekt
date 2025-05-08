using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Konnekt.Client.Migrations
{
    /// <inheritdoc />
    public partial class AddingSortingFrameworkForServers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServerOrder",
                table: "ServerUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServerOrder",
                table: "ServerUsers");

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
    }
}
