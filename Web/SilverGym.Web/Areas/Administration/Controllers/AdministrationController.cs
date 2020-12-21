namespace SilverGym.Web.Areas.Administration.Controllers
{
    using SilverGym.Common;
    using SilverGym.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SilverGym.Web.ViewModels.Administration;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        public AdministrationController()
        {

        }


    }
}
