namespace SilverGym.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SilverGym.Common;
    using SilverGym.Services.Data.Contracts;
    using SilverGym.Web.ViewModels;

    [Authorize(Roles = GlobalConstants.TrainerRoleName)]
    [Area("Trainer")]
    public class TrainerController : BaseController
    {
        private readonly ITrainersService trainersService;

        public TrainerController(ITrainersService trainersService)
        {
            this.trainersService = trainersService;
        }

        public IActionResult ControlPanel()
        {
            return this.View();
        }

        public IActionResult AddClient()
        {
            var viewModel = new AddClientToTrainerInputModel()
            {
                TrainerId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(AddClientToTrainerInputModel input)
        {
            try
            {
                await this.trainersService.AddClient(input);
            }
            catch (Exception e)
            {
                 this.ModelState.AddModelError("client", e.Message);
            }

            if (!this.ModelState.IsValid)
            {
                var viewModel = new AddClientToTrainerInputModel()
                {
                    TrainerId = this.User.FindFirstValue(ClaimTypes.NameIdentifier),
                };
                return this.View(viewModel);
            }

            return this.Redirect("/Trainer/Trainer/ControlPanel");
        }
    }
}
