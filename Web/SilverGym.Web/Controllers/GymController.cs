namespace SilverGym.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SilverGym.Services.Data.Contracts;
    using SilverGym.Web.ViewModels.Trainer;
    using System;
    using System.Threading.Tasks;

    public class GymController : BaseController
    {
        private readonly ITrainersService trainersService;

        public GymController(ITrainersService trainersService)
        {
            this.trainersService = trainersService;
        }

        public IActionResult Gallery()
        {
            return this.View();
        }

        public async Task<IActionResult> Trainers()
        {
            var viewModel = await this.trainersService.GetTrainers();
            return this.View(viewModel);
        }

        public async Task<IActionResult> Trainer(string id)
        {
            var viewModel = new TrainerViewModel();

            try
            {
                viewModel = await this.trainersService.GetTrainer(id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("trainer", e.Message);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            return this.View(viewModel);
        }
    }
}
