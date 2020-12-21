namespace SilverGym.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddClientToTrainerInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string TrainerId { get; set; }
    }
}
