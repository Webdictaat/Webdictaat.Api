using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Webdictaat.Api.Migrations
{
    public partial class polls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Polls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DictaatName = table.Column<string>(nullable: false),
                    Question = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PollOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PollId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollOption_Polls_PollId",
                        column: x => x.PollId,
                        principalTable: "Polls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PollVotes",
                columns: table => new
                {
                    PollId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    PollOptionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollVotes", x => new { x.PollId, x.UserId });
                    table.ForeignKey(
                        name: "FK_PollVotes_PollOption_PollOptionId",
                        column: x => x.PollOptionId,
                        principalTable: "PollOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Polls_DictaatDetails_DictaatName",
                table: "Polls",
                column: "DictaatName",
                principalTable: "DictaatDetails",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);


            migrationBuilder.CreateIndex(
                name: "IX_PollOption_PollId",
                table: "PollOption",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_PollVotes_PollOptionId",
                table: "PollVotes",
                column: "PollOptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PollVotes");

            migrationBuilder.DropTable(
                name: "PollOption");

            migrationBuilder.DropTable(
                name: "Polls");
        }
    }
}
