using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AymanProject.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainCriterians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Text_Ar = table.Column<string>(type: "TEXT", nullable: false),
                    Text_En = table.Column<string>(type: "TEXT", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCriterians", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    SubmittedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCriterians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MainId = table.Column<int>(type: "INTEGER", nullable: false),
                    Text_Ar = table.Column<string>(type: "TEXT", nullable: false),
                    Text_En = table.Column<string>(type: "TEXT", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCriterians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCriterians_MainCriterians_MainId",
                        column: x => x.MainId,
                        principalTable: "MainCriterians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMainCriterians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    MainCriterianId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserEvaluation = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMainCriterians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMainCriterians_MainCriterians_MainCriterianId",
                        column: x => x.MainCriterianId,
                        principalTable: "MainCriterians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMainCriterians_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSubCriterians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectMainCriterianId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubCriterianId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserEvaluation = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSubCriterians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectSubCriterians_ProjectMainCriterians_ProjectMainCriterianId",
                        column: x => x.ProjectMainCriterianId,
                        principalTable: "ProjectMainCriterians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSubCriterians_SubCriterians_SubCriterianId",
                        column: x => x.SubCriterianId,
                        principalTable: "SubCriterians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMainCriterians_MainCriterianId",
                table: "ProjectMainCriterians",
                column: "MainCriterianId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMainCriterians_ProjectId",
                table: "ProjectMainCriterians",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSubCriterians_ProjectMainCriterianId",
                table: "ProjectSubCriterians",
                column: "ProjectMainCriterianId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSubCriterians_SubCriterianId",
                table: "ProjectSubCriterians",
                column: "SubCriterianId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCriterians_MainId",
                table: "SubCriterians",
                column: "MainId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectSubCriterians");

            migrationBuilder.DropTable(
                name: "ProjectMainCriterians");

            migrationBuilder.DropTable(
                name: "SubCriterians");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "MainCriterians");
        }
    }
}
