namespace SilverGym.Web.ViewModels.EatingPlan
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SilverGym.Web.ViewModels.MealPlan;

    public class EatingPlanInputModel
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public List<MealPlanInputModel> MealPlans { get; set; }
    }
}
