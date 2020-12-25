namespace SilverGym.Web.ViewModels.WorkDays
{
    using System.Collections.Generic;

    using SilverGym.Data.Models.Enums;
    using SilverGym.Web.ViewModels.Exercises;

    public class WorkoutDayViewModel
    {
        public string WorkDay { get; set; }

        public List<ExerciseViewModel> Exercises { get; set; }
    }
}
