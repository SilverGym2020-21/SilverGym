﻿namespace SilverGym.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SilverGym.Services.Data.Contracts;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ProfileController : BaseController
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        public IActionResult Home()
        {
            return this.View();
        }

        public IActionResult Training()
        {
            return this.View();
        }

        public IActionResult Eating()
        {
            return this.View();
        }

        public async Task<IActionResult> Trainer()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = await this.profileService.GetTrainer(userId);
            return this.View(viewModel);
        }
    }
}
