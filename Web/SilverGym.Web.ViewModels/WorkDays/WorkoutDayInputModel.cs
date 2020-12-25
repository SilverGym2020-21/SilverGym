﻿namespace SilverGym.Web.ViewModels.WorkDays
{
    using System.Collections.Generic;

    using SilverGym.Data.Models.Enums;
    using SilverGym.Web.ViewModels.Exercises;

    public class WorkoutDayInputModel
    {
        public WorkDay WorkDay { get; set; }

        public List<ExerciseInputModel> Exercises { get; set; }
    }
}
