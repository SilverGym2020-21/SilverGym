namespace SilverGym.Web.ViewModels.WorkoutPlan
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SilverGym.Web.ViewModels.WorkDays;

    public class WorkoutPlanInputModel
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public List<WorkoutDayInputModel> WorkoutDays { get; set; }
    }
}
