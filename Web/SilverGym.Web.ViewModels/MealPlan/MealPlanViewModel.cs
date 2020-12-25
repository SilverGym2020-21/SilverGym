namespace SilverGym.Web.ViewModels.MealPlan
{
    using System.Collections.Generic;

    using SilverGym.Web.ViewModels.Meals;

    public class MealPlanViewModel
    {
        public string Name { get; set; }

        public string Time { get; set; }

        public List<MealViewModel> Meals { get; set; }
    }
}
