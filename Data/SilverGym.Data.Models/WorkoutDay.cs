namespace SilverGym.Data.Models
{
    using System;
    using System.Collections.Generic;

    using SilverGym.Data.Common.Models;
    using SilverGym.Data.Models.Enums;

    public class WorkoutDay : IDeletableEntity
    {
        public WorkoutDay()
        {
            this.WorkoutDayId = Guid.NewGuid().ToString();
            this.Exercises = new HashSet<Exercise>();
        }

        public string WorkoutDayId { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }

        public WorkDay WorkDay { get; set; }

        public virtual WorkoutPlan WorkoutPlan { get; set; }

        public virtual string WorkoutPlanId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
