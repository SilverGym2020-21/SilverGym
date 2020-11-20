using Microsoft.AspNetCore.Mvc;

namespace SilverGym.Web.Controllers
{
    public class GymController : BaseController
    {
        public GymController()
        {
        }

        public IActionResult Gallery()
        {
            return this.View();
        }

        public IActionResult Trainers()
        {
            return this.View();
        }
    }
}
