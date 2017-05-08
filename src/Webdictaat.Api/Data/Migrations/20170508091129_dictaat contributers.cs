using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Webdictaat.Api.Migrations
{
    public partial class dictaatcontributers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DictaatDetails_AspNetUsers_DictaatOwnerId",
                table: "DictaatDetails");

            migrationBuilder.DropIndex(
                name: "IX_DictaatDetails_DictaatOwnerId",
                table: "DictaatDetails");

            migrationBuilder.RenameColumn(
                name: "Secret",
                table: "Assignments",
                newName: "AssignmentSecret");

            migrationBuilder.AlterColumn<string>(
                name: "DictaatOwnerId",
                table: "DictaatDetails",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "DictaatOwnersId",
                table: "DictaatDetails",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Assignments",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "DictaatContributer",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    DictaatDetailsId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictaatContributer", x => new { x.UserId, x.DictaatDetailsId });
                    table.ForeignKey(
                        name: "FK_DictaatContributer_DictaatDetails_DictaatDetailsId",
                        column: x => x.DictaatDetailsId,
                        principalTable: "DictaatDetails",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DictaatContributer_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DictaatDetails_DictaatOwnersId",
                table: "DictaatDetails",
                column: "DictaatOwnersId");

            migrationBuilder.CreateIndex(
                name: "IX_DictaatContributer_DictaatDetailsId",
                table: "DictaatContributer",
                column: "DictaatDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DictaatDetails_AspNetUsers_DictaatOwnersId",
                table: "DictaatDetails",
                column: "DictaatOwnersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DictaatDetails_AspNetUsers_DictaatOwnersId",
                table: "DictaatDetails");

            migrationBuilder.DropTable(
                name: "DictaatContributer");

            migrationBuilder.DropIndex(
                name: "IX_DictaatDetails_DictaatOwnersId",
                table: "DictaatDetails");

            migrationBuilder.DropColumn(
                name: "DictaatOwnersId",
                table: "DictaatDetails");

            migrationBuilder.RenameColumn(
                name: "AssignmentSecret",
                table: "Assignments",
                newName: "Secret");

            migrationBuilder.AlterColumn<string>(
                name: "DictaatOwnerId",
                table: "DictaatDetails",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Assignments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DictaatDetails_DictaatOwnerId",
                table: "DictaatDetails",
                column: "DictaatOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DictaatDetails_AspNetUsers_DictaatOwnerId",
                table: "DictaatDetails",
                column: "DictaatOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
