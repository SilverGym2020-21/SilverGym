namespace SilverGym.Web.ViewModels.Trainer
{
    using System.ComponentModel.DataAnnotations;

    public class RemoveClientFromTrainerInputModel
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        [EmailAddress]
        public string ClientEmail { get; set; }

        [Required]
        public string TrainerId { get; set; }
    }
}
