namespace SilverGym.Web.ViewModels.Meals
{
    using System.ComponentModel.DataAnnotations;

    public class MealInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string GramsOrMil { get; set; }
    }
}
