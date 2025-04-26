using System.Diagnostics;
using AymanProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AymanProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string lang = "en")
        {
            if (!string.IsNullOrEmpty(lang))
            {
                Response.Cookies.Append("Lang", lang, new CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1),
                    IsEssential = true
                });
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
