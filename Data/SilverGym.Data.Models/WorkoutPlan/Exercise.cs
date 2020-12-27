namespace SilverGym.Data.Models
{
    using System;

    using SilverGym.Data.Common.Models;
    using SilverGym.Data.Models.Enums;

    public class Exercise : IDeletableEntity
    {
        public Exercise()
        {
            this.ExerciseId = Guid.NewGuid().ToString();
        }

        public string ExerciseId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string Name { get; set; }

        public MuscleGroup MuscleGroup { get; set; }

        public string RepsOrTime { get; set; }

        public virtual WorkoutDay WorkoutDay { get; set; }

        public virtual string WorkoutDayId { get; set; }
    }
}
