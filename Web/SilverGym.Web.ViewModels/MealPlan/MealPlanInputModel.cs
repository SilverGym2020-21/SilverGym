namespace SilverGym.Web.ViewModels.MealPlan
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SilverGym.Web.ViewModels.Meals;

    public class MealPlanInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Time { get; set; }

        public List<MealInputModel> Meals { get; set; }
    }
}
