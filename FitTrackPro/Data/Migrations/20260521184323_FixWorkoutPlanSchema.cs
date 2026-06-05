using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrackPro.Data.Migrations
{
        public partial class FixWorkoutPlanSchema : Migration
    {
                protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "WorkoutPlanExercises",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

                protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "WorkoutPlanExercises");
        }
    }
}
