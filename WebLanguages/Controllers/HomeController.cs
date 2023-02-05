using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebLanguages.Models;

using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using System.Net;

namespace WebLanguages.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Variable for get resources from controller
        private readonly IStringLocalizer<HomeController> _getResource;
        public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> getResource)
        {
            _logger = logger;
            _getResource = getResource;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["textInfo"] = _getResource["textInfo"];        // Create reference for view
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        // Function for save the idiom on the cookie
        public IActionResult SetCulture(string newCulture, string returnUrl) 
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(newCulture)),   // Make a new cookie
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) }        // Cookie expiration time
                );

            return LocalRedirect(returnUrl);
        }
    }
}