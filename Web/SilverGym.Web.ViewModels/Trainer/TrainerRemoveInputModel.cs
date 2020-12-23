namespace SilverGym.Web.ViewModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    public class TrainerRemoveInputModel
    {
        [Required]
        public string Email { get; set; }
    }
}
