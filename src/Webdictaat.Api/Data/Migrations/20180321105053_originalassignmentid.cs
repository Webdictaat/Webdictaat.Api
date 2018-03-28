using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Webdictaat.Api.Migrations
{
    public partial class originalassignmentid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DictaatDetailsName",
                table: "Quizes",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Quizes_DictaatDetailsName",
                table: "Quizes",
                column: "DictaatDetailsName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Quizes_DictaatDetailsName",
                table: "Quizes");

            migrationBuilder.DropIndex(
                name: "IX_Polls_DictaatName",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "DictaatName",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "OriginalAssignmentId",
                table: "Assignments");

            migrationBuilder.AlterColumn<string>(
                name: "DictaatDetailsName",
                table: "Quizes",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
