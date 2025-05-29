using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Konnekt.Client.Migrations
{
    /// <inheritdoc />
    public partial class AddingDatesToMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimeEdited",
                table: "ServerMessage",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeSent",
                table: "ServerMessage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "743da3b6-e3a4-40fb-ae3a-6773b103ee1a",
                column: "ConcurrencyStamp",
                value: "635b2d3b-39d1-48c1-8346-5aa55e1326bc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc3323c1-5f95-4a9b-803f-983c5a6a537e",
                column: "ConcurrencyStamp",
                value: "a7998246-3d28-4c7b-8a53-183268c8d5c9");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeEdited",
                table: "ServerMessage");

            migrationBuilder.DropColumn(
                name: "TimeSent",
                table: "ServerMessage");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "743da3b6-e3a4-40fb-ae3a-6773b103ee1a",
                column: "ConcurrencyStamp",
                value: "53935d54-245f-4b09-8655-3b4e84fb42f7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc3323c1-5f95-4a9b-803f-983c5a6a537e",
                column: "ConcurrencyStamp",
                value: "7b2d72ca-4ba0-4a43-814f-90a84431bb4e");
        }
    }
}
