namespace SilverGym.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ProfileController : BaseController
    {
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

        public IActionResult Trainer()
        {
            return this.View();
        }
    }
}
