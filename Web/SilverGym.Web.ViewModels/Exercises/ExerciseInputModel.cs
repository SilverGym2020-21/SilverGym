namespace SilverGym.Web.ViewModels.Exercises
{
    using System.ComponentModel.DataAnnotations;

    using SilverGym.Data.Models.Enums;

    public class ExerciseInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public MuscleGroup MuscleGroup { get; set; }

        [Required]
        public string RepsOrTime { get; set; }
    }
}
