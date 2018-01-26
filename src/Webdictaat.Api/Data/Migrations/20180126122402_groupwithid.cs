using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Webdictaat.Api.Migrations
{
    public partial class groupwithid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Group",
                table: "DictaatSessionUser",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DictaatName",
                table: "DictaatSessionUser",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DictaatGroup",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    DictaatName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictaatGroup", x => new { x.Name, x.DictaatName });
                    table.ForeignKey(
                        name: "FK_DictaatGroup_DictaatDetails_DictaatName",
                        column: x => x.DictaatName,
                        principalTable: "DictaatDetails",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DictaatSessionUser_Group_DictaatName",
                table: "DictaatSessionUser",
                columns: new[] { "Group", "DictaatName" });

            migrationBuilder.CreateIndex(
                name: "IX_DictaatGroup_DictaatName",
                table: "DictaatGroup",
                column: "DictaatName");

            migrationBuilder.AddForeignKey(
                name: "FK_DictaatSessionUser_DictaatGroup_Group_DictaatName",
                table: "DictaatSessionUser",
                columns: new[] { "Group", "DictaatName" },
                principalTable: "DictaatGroup",
                principalColumns: new[] { "Name", "DictaatName" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DictaatSessionUser_DictaatGroup_Group_DictaatName",
                table: "DictaatSessionUser");

            migrationBuilder.DropTable(
                name: "DictaatGroup");

            migrationBuilder.DropIndex(
                name: "IX_DictaatSessionUser_Group_DictaatName",
                table: "DictaatSessionUser");

            migrationBuilder.DropColumn(
                name: "DictaatName",
                table: "DictaatSessionUser");

            migrationBuilder.AlterColumn<string>(
                name: "Group",
                table: "DictaatSessionUser",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
