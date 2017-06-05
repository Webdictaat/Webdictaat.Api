using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Webdictaat.Api.Migrations
{
    public partial class dictaat_sessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DictaatSession",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DictaatDetailsId = table.Column<string>(nullable: true),
                    EndedOn = table.Column<DateTime>(nullable: false),
                    StartedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictaatSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DictaatSession_DictaatDetails_DictaatDetailsId",
                        column: x => x.DictaatDetailsId,
                        principalTable: "DictaatDetails",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DictaatSessionUser",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    DictaatSessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictaatSessionUser", x => new { x.UserId, x.DictaatSessionId });
                    table.ForeignKey(
                        name: "FK_DictaatSessionUser_DictaatSession_DictaatSessionId",
                        column: x => x.DictaatSessionId,
                        principalTable: "DictaatSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DictaatSessionUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DictaatSession_DictaatDetailsId",
                table: "DictaatSession",
                column: "DictaatDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_DictaatSessionUser_DictaatSessionId",
                table: "DictaatSessionUser",
                column: "DictaatSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DictaatSessionUser");

            migrationBuilder.DropTable(
                name: "DictaatSession");
        }
    }
}
