using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Webdictaat.Api.Migrations
{
    public partial class RemoveAnswerAddBody : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizAttemptAnswer");

            migrationBuilder.AddColumn<bool>(
                name: "Shuffle",
                table: "Quizes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuestionType",
                table: "Questions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuizAttemptQuestion",
                columns: table => new
                {
                    QuizAttemptId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    IsCorrect = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAttemptQuestion", x => new { x.QuizAttemptId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_QuizAttemptQuestion_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizAttemptQuestion_QuizAttempts_QuizAttemptId",
                        column: x => x.QuizAttemptId,
                        principalTable: "QuizAttempts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttemptQuestion_QuestionId",
                table: "QuizAttemptQuestion",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizAttemptQuestion");

            migrationBuilder.DropColumn(
                name: "Shuffle",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionType",
                table: "Questions");

            migrationBuilder.CreateTable(
                name: "QuizAttemptAnswer",
                columns: table => new
                {
                    QuizAttemptId = table.Column<int>(nullable: false),
                    AnswerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAttemptAnswer", x => new { x.QuizAttemptId, x.AnswerId });
                    table.ForeignKey(
                        name: "FK_QuizAttemptAnswer_Answer_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizAttemptAnswer_QuizAttempts_QuizAttemptId",
                        column: x => x.QuizAttemptId,
                        principalTable: "QuizAttempts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttemptAnswer_AnswerId",
                table: "QuizAttemptAnswer",
                column: "AnswerId");
        }
    }
}
