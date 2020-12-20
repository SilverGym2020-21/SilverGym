using Microsoft.EntityFrameworkCore.Migrations;

namespace SilverGym.Data.Migrations
{
    public partial class MealsAndExerciseFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "MealPlan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "MealPlan",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
