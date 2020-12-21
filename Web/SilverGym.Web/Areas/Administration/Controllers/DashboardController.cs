namespace SilverGym.Web.Areas.Administration.Controllers
{
    using SilverGym.Services.Data;
    using SilverGym.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Mvc;
    using SilverGym.Web.ViewModels.Administration;
    using SilverGym.Services.Data.Contracts;
    using System.Threading.Tasks;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly IAdministrationService administrationService;

        public DashboardController(ISettingsService settingsService, IAdministrationService administrationService)
        {
            this.settingsService = settingsService;
            this.administrationService = administrationService;
        }

    }
}
