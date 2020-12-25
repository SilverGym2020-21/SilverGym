using Microsoft.EntityFrameworkCore.Migrations;

namespace SilverGym.Data.Migrations
{
    public partial class ExerciseModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reps",
                table: "Exercises");

            migrationBuilder.AddColumn<string>(
                name: "RepsOrTime",
                table: "Exercises",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepsOrTime",
                table: "Exercises");

            migrationBuilder.AddColumn<int>(
                name: "Reps",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
