namespace SilverGym.Web.ViewModels.WorkoutPlan
{
    using System.ComponentModel.DataAnnotations;

    public class WorkoutPlanInputModel
    {
        [Required]
        public string ClientId { get; set; }
    }
}
