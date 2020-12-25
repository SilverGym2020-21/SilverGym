namespace SilverGym.Web.ViewModels.WorkoutPlan
{
    using System.Collections.Generic;

    using SilverGym.Web.ViewModels.WorkDays;

    public class WorkoutPlanViewModel
    {
        public List<WorkoutDayViewModel> WorkoutDays { get; set; }
    }
}
