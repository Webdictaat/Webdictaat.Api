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
            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DictaatName = table.Column<string>(nullable: true),
                    Hidden = table.Column<bool>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DictaatAchievements",
                columns: table => new
                {
                    DictaatName = table.Column<string>(nullable: false),
                    AchievementId = table.Column<int>(nullable: false),
                    GroupName = table.Column<string>(nullable: false),
                    GroupOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictaatAchievements", x => new { x.DictaatName, x.AchievementId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "DictaatAchievements");
        }
    }
}
