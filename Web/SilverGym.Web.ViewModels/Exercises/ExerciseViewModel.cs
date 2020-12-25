namespace SilverGym.Web.ViewModels.Exercises
{
    using SilverGym.Data.Models.Enums;

    public class ExerciseViewModel
    {
        public string Name { get; set; }

        public MuscleGroup MuscleGroup { get; set; }

        public string RepsOrTime { get; set; }
    }
}
