namespace SilverGym.Data.Models
{
    using System;
    using System.Collections.Generic;

    using SilverGym.Data.Common.Models;

    public class Meal : IDeletableEntity
    {
        public Meal()
        {
            this.MealId = Guid.NewGuid().ToString();
            this.Plans = new HashSet<MealPlans>();
        }

        public string MealId { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<MealPlans> Plans { get; set; }
    }
}
