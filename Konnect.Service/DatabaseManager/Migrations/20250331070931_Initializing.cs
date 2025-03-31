using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Konnekt.Client.Migrations
{
    /// <inheritdoc />
    public partial class Initializing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostComments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR(36)", nullable: false),
                    CommentContent = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    CommentAuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostComments_AspNetUsers_CommentAuthorId",
                        column: x => x.CommentAuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR(36)", nullable: false),
                    PostTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PostContent = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    PostAuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_PostAuthorId",
                        column: x => x.PostAuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR(36)", nullable: false),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerJoinCodes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR(36)", nullable: false),
                    JoinCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServerId = table.Column<string>(type: "NVARCHAR(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerJoinCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerJoinCodes_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServerUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR(36)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ServerId = table.Column<string>(type: "NVARCHAR(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServerUsers_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "743da3b6-e3a4-40fb-ae3a-6773b103ee1a", "f8c0f84f-f021-45f8-9ef3-c58f4424fd44", "Admin", "ADMIN" },
                    { "dc3323c1-5f95-4a9b-803f-983c5a6a537e", "945d19cf-d496-4847-8015-2cb5d57d65c1", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_CommentAuthorId",
                table: "PostComments",
                column: "CommentAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_Id",
                table: "PostComments",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Id",
                table: "Posts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostAuthorId",
                table: "Posts",
                column: "PostAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerJoinCodes_Id",
                table: "ServerJoinCodes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServerJoinCodes_ServerId",
                table: "ServerJoinCodes",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_Id",
                table: "Servers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServerUsers_Id",
                table: "ServerUsers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServerUsers_ServerId",
                table: "ServerUsers",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerUsers_UserId",
                table: "ServerUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostComments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "ServerJoinCodes");

            migrationBuilder.DropTable(
                name: "ServerUsers");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "743da3b6-e3a4-40fb-ae3a-6773b103ee1a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc3323c1-5f95-4a9b-803f-983c5a6a537e");
        }
    }
}
