namespace SilverGym.Data.Models
{
    using System;
    using System.Collections.Generic;

    using SilverGym.Data.Common.Models;

    public class MealPlan : IDeletableEntity
    {
        public MealPlan()
        {
            this.MealPlanId = Guid.NewGuid().ToString();
            this.Meals = new HashSet<MealPlans>();
        }

        public string MealPlanId { get; set; }

        public string Name { get; set; }

        public string Time { get; set; }

        public virtual ICollection<MealPlans> Meals { get; set; }

        public virtual EatingPlan EatingPlan { get; set; }

        public virtual string EatingPlanId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}