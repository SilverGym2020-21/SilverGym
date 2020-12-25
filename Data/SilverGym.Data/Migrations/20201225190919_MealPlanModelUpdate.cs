using Microsoft.EntityFrameworkCore.Migrations;

namespace SilverGym.Data.Migrations
{
    public partial class MealPlanModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealPlans");

            migrationBuilder.AddColumn<string>(
                name: "GramsOrMil",
                table: "Meals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MealPlanId",
                table: "Meals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meals_MealPlanId",
                table: "Meals",
                column: "MealPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_MealPlan_MealPlanId",
                table: "Meals",
                column: "MealPlanId",
                principalTable: "MealPlan",
                principalColumn: "MealPlanId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_MealPlan_MealPlanId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_MealPlanId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "GramsOrMil",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "MealPlanId",
                table: "Meals");

            migrationBuilder.CreateTable(
                name: "MealPlans",
                columns: table => new
                {
                    MealId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MealPlanId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlans", x => new { x.MealId, x.MealPlanId });
                    table.ForeignKey(
                        name: "FK_MealPlans_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealPlans_MealPlan_MealPlanId",
                        column: x => x.MealPlanId,
                        principalTable: "MealPlan",
                        principalColumn: "MealPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealPlans_MealPlanId",
                table: "MealPlans",
                column: "MealPlanId");
        }
    }
}
