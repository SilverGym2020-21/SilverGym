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
    using SilverGym.Web.ViewModels.EatingPlan;
    using SilverGym.Web.ViewModels.Trainer;
    using SilverGym.Web.ViewModels.WorkDays;
    using SilverGym.Web.ViewModels.WorkoutPlan;

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

        public async Task<IActionResult> Clients()
        {
            var trainerId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = await this.trainersService.GetClients(trainerId);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveClient(RemoveClientFromTrainerInputModel input)
        {
            try
            {
                await this.trainersService.RemoveClient(input);
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

        public IActionResult AddEatingPlan(string id)
        {
            this.ViewData["ClientId"] = id;
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEatingPlan(EatingPlanInputModel input)
        {
            await this.trainersService.AddEatingPlantToClient(input);
            return this.Redirect("/Trainer/Trainer/Clients");
        }

        public async Task<IActionResult> RemoveEatingPlan(string id)
        {
            var trainerId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.trainersService.RemoveEatingPlantFromClient(id, trainerId);
            return this.Redirect("/Trainer/Trainer/Clients");
        }

        public IActionResult AddWorkoutPlan(string id)
        {
            this.ViewData["ClientId"] = id;
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkoutPlan(WorkoutPlanInputModel input)
        {
            await this.trainersService.AddWorkoutPlantToClient(input);
            return this.Redirect("/Trainer/Trainer/Clients");
        }

        public async Task<IActionResult> RemoveWorkoutPlan(string id)
        {
            var trainerId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.trainersService.RemoveWorkoutPlantFromClient(id, trainerId);
            return this.Redirect("/Trainer/Trainer/Clients");
        }
    }
}
