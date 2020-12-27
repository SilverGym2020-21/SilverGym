namespace SilverGym.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SilverGym.Common;
    using SilverGym.Services.Data.Contracts;
    using SilverGym.Web.ViewModels.Administration;
    using SilverGym.Web.ViewModels.Products;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : Controller
    {
        private readonly IAdministrationService administrationService;

        public AdministrationController(IAdministrationService administrationService)
        {
            this.administrationService = administrationService;
        }

        public IActionResult AdminPanel()
        {
            return this.View();
        }

        public IActionResult AddTrainer()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTrainer(TrainerInputModel input)
        {
            try
            {
                await this.administrationService.AddTrainer(input);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("admin", e.Message);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            return this.Redirect("/");
        }

        public IActionResult RemoveTrainer()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTrainer(TrainerRemoveInputModel input)
        {
            try
            {
                await this.administrationService.RemoveTrainer(input);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("trainer", e.Message);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            return this.Redirect("/");
        }

        public IActionResult AddProduct()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductInputModel input)
        {
            try
            {
                await this.administrationService.AddProduct(input);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("admin", e.Message);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            return this.Redirect("/");
        }
    }
}
