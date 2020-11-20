namespace SilverGym.Web.Controllers
{
    using System.Diagnostics;

    using SilverGym.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return this.View("Home");
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult TOS()
        {
            return this.View();
        }

        public IActionResult Facebook()
        {
            return this.Redirect("https://www.facebook.com/FitnessSilverGym");
        }

        public IActionResult Instagram()
        {
            return this.Redirect("https://github.com/SilverGym2020-21/SilverGym");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
