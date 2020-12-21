namespace SilverGym.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SilverGym.Common;
    using SilverGym.Services.Data.Contracts;
    using SilverGym.Web.ViewModels.Administration;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
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
            await this.administrationService.AddTrainer(input);
            return this.Redirect("/");
        }

        public IActionResult RemoveTrainer()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTrainer(TrainerInputModel input)
        {
            await this.administrationService.RemoveTrainer(input);
            return this.Redirect("/");
        }

    }
}
