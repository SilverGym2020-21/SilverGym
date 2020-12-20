namespace SilverGym.Data.Models
{
    using System;
    using System.Collections.Generic;

    using SilverGym.Data.Common.Models;

    public class EatingPlan : IDeletableEntity
    {
        public EatingPlan()
        {
            this.EatingPlanId = Guid.NewGuid().ToString();
            this.MealPlans = new HashSet<MealPlan>();
        }

        public string EatingPlanId { get; set; }

        public virtual ICollection<MealPlan> MealPlans { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual string UserId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}