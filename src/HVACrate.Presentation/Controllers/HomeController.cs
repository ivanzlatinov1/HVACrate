using HVACrate.Presentation.Extensions;
using HVACrate.Presentation.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HVACrate.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.IsAuthenticated() == false)
            {
                return View();
            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction(nameof(Index), "Users");
            }
            else
            {
                return RedirectToAction(nameof(Index), "Projects");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode, string? errorMessage)
        {
            ViewData["StatusCode"] = statusCode?.ToString() ?? "Unknown";

            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                ErrorMessage = errorMessage ?? "The page you are looking for does not exist."
            };

            return View(errorViewModel);
        }
    }
}
