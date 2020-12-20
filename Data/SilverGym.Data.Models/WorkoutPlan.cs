namespace SilverGym.Data.Models
{
    using System;
    using System.Collections.Generic;

    using SilverGym.Data.Common.Models;

    public class WorkoutPlan : IDeletableEntity
    {
        public WorkoutPlan()
        {
            this.WorkoutPlanId = Guid.NewGuid().ToString();
            this.WorkoutDays = new HashSet<WorkoutDay>();
        }

        public string WorkoutPlanId { get; set; }

        public virtual ICollection<WorkoutDay> WorkoutDays { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual string UserId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
