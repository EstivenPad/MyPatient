using Microsoft.AspNetCore.Mvc;
using MyPatient.Models;
using System.Diagnostics;

namespace MyPatient.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/StatusCodeError")]
        public IActionResult Error(int statusCode)
        {
            if(statusCode == 404)
            {
                ViewBag.ErrorMessage = "404";
            }

            ViewData["Title"] = "Error 404";

            return View("NotFound");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
