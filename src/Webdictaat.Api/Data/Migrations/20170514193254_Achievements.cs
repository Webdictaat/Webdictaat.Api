using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Webdictaat.Api.Migrations
{
    public partial class Achievements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DictaatDetails_AspNetUsers_DictaatOwnersId",
                table: "DictaatDetails");

            migrationBuilder.DropIndex(
                name: "IX_DictaatDetails_DictaatOwnersId",
                table: "DictaatDetails");

            migrationBuilder.DropColumn(
                name: "DictaatOwnersId",
                table: "DictaatDetails");

            migrationBuilder.AlterColumn<string>(
                name: "DictaatOwnerId",
                table: "DictaatDetails",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Hidden = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempts_QuizId",
                table: "QuizAttempts",
                column: "QuizId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_QuizAttempts_Quizes_QuizId",
                table: "QuizAttempts",
                column: "QuizId",
                principalTable: "Quizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DictaatDetails_AspNetUsers_DictaatOwnerId",
                table: "DictaatDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizAttempts_Quizes_QuizId",
                table: "QuizAttempts");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropIndex(
                name: "IX_QuizAttempts_QuizId",
                table: "QuizAttempts");

            migrationBuilder.DropIndex(
                name: "IX_DictaatDetails_DictaatOwnerId",
                table: "DictaatDetails");

            migrationBuilder.AlterColumn<string>(
                name: "DictaatOwnerId",
                table: "DictaatDetails",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "DictaatOwnersId",
                table: "DictaatDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DictaatDetails_DictaatOwnersId",
                table: "DictaatDetails",
                column: "DictaatOwnersId");

            migrationBuilder.AddForeignKey(
                name: "FK_DictaatDetails_AspNetUsers_DictaatOwnersId",
                table: "DictaatDetails",
                column: "DictaatOwnersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
