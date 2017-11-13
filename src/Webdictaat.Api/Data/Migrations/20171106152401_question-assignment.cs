using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Webdictaat.Api.Migrations
{
    public partial class questionassignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignmentId",
                table: "Quizes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssignmentType",
                table: "Assignments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quizes_AssignmentId",
                table: "Quizes",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizes_Assignments_AssignmentId",
                table: "Quizes",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizes_Assignments_AssignmentId",
                table: "Quizes");

            migrationBuilder.DropIndex(
                name: "IX_Quizes_AssignmentId",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "AssignmentType",
                table: "Assignments");
        }
    }
}
