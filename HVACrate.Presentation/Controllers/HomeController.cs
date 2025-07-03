using HVACrate.Presentation.Extensions;
using HVACrate.Presentation.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HVACrate.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if(User.IsAuthenticated() && User.IsInRole("Admin"))
            {
                return RedirectToAction(nameof(Index), "Users");
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
