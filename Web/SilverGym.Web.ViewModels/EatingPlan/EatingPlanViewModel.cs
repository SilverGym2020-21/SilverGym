namespace SilverGym.Web.ViewModels.EatingPlan
{
    using System.Collections.Generic;

    using SilverGym.Web.ViewModels.MealPlan;

    public class EatingPlanViewModel
    {
        public List<MealPlanViewModel> MealPlans { get; set; }
    }
}
