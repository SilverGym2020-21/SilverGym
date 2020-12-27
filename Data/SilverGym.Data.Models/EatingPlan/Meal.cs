namespace SilverGym.Data.Models
{
    using System;

    using SilverGym.Data.Common.Models;

    public class Meal : IDeletableEntity
    {
        public Meal()
        {
            this.MealId = Guid.NewGuid().ToString();
        }

        public string MealId { get; set; }

        public string Name { get; set; }

        public string GramsOrMil { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual string MealPlanId { get; set; }

        public virtual MealPlan MealPlan { get; set; }
    }
}
