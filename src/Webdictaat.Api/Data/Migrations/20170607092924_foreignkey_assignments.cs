using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Webdictaat.Api.Migrations
{
    public partial class foreignkey_assignments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DictaatDetailsName",
                table: "Assignments",
                newName: "DictaatDetailsId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartedOn",
                table: "DictaatSession",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndedOn",
                table: "DictaatSession",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DictaatDetailsId",
                table: "Assignments",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_DictaatAchievements_AchievementId",
                table: "DictaatAchievements",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_DictaatDetailsId",
                table: "Assignments",
                column: "DictaatDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_DictaatDetails_DictaatDetailsId",
                table: "Assignments",
                column: "DictaatDetailsId",
                principalTable: "DictaatDetails",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DictaatAchievements_Achievements_AchievementId",
                table: "DictaatAchievements",
                column: "AchievementId",
                principalTable: "Achievements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DictaatAchievements_DictaatDetails_DictaatName",
                table: "DictaatAchievements",
                column: "DictaatName",
                principalTable: "DictaatDetails",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_DictaatDetails_DictaatDetailsId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_DictaatAchievements_Achievements_AchievementId",
                table: "DictaatAchievements");

            migrationBuilder.DropForeignKey(
                name: "FK_DictaatAchievements_DictaatDetails_DictaatName",
                table: "DictaatAchievements");

            migrationBuilder.DropIndex(
                name: "IX_DictaatAchievements_AchievementId",
                table: "DictaatAchievements");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_DictaatDetailsId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "DictaatDetailsId",
                table: "Assignments",
                newName: "DictaatDetailsName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartedOn",
                table: "DictaatSession",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndedOn",
                table: "DictaatSession",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "DictaatDetailsName",
                table: "Assignments",
                nullable: false);
        }
    }
}
