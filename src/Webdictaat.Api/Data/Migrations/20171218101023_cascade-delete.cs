using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Webdictaat.Api.Migrations
{
    public partial class cascadedelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            //add cascade delete to dictaatdetails
            //foregn key between quiz and assignment
            migrationBuilder.DropForeignKey(
                    name: "FK_Quizes_Assignments_AssignmentId",
                     table: "Quizes"
                );

            migrationBuilder.AddForeignKey(
                name: "FK_Quizes_Assignments_AssignmentId",
                table: "Quizes",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            //foreign key between dictaationsessions and dictaatdetails
            migrationBuilder.DropForeignKey(
                name: "FK_DictaatSession_DictaatDetails_DictaatDetailsId",
                table: "DictaatSession"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_DictaatSession_DictaatDetails_DictaatDetailsId",
                table: "DictaatSession",
                column: "DictaatDetailsId",
                principalTable: "DictaatDetails",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            //relation between quiz and quizattempt
            migrationBuilder.DropForeignKey(
                name: "FK_QuizAttempts_Quizes_QuizId",
                table: "QuizAttempts"
            );

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
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
