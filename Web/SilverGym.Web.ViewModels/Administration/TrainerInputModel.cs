﻿namespace SilverGym.Web.ViewModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    public class TrainerInputModel
    {
        [Required]
        public string Email { get; set; }
    }
}
