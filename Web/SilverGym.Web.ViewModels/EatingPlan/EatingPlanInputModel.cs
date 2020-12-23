using System.ComponentModel.DataAnnotations;

namespace SilverGym.Web.ViewModels.EatingPlan
{
    public class EatingPlanInputModel
    {
        [Required]
        public string ClientId { get; set; }
    }
}
