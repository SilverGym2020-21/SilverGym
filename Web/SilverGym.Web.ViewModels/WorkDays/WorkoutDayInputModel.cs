namespace SilverGym.Web.ViewModels.WorkDays
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using SilverGym.Data.Models.Enums;
    using SilverGym.Web.ViewModels.Exercises;

    public class WorkoutDayInputModel
    {
        [Required]
        public WorkDay WorkDay { get; set; }

        [Required]
        public List<ExerciseInputModel> Exercises { get; set; }
    }
}
