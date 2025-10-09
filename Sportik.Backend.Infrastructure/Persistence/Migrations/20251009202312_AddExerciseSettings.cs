using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sportik.Backend.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddExerciseSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SettingsId",
                table: "Exercises",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ExerciseSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TargetRepetitions = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeBetweenSets = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    ExecutionTime = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_SettingsId",
                table: "Exercises",
                column: "SettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_ExerciseSettings_SettingsId",
                table: "Exercises",
                column: "SettingsId",
                principalTable: "ExerciseSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_ExerciseSettings_SettingsId",
                table: "Exercises");

            migrationBuilder.DropTable(
                name: "ExerciseSettings");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_SettingsId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "SettingsId",
                table: "Exercises");
        }
    }
}
