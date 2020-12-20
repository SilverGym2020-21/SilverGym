namespace SilverGym.Data.Models
{
    public class MealPlans
    {
        public string MealPlanId { get; set; }

        public virtual MealPlan MealPlan { get; set; }

        public string MealId { get; set; }

        public virtual Meal Meal { get; set; }
    }
}
