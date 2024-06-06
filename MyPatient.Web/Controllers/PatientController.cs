using Microsoft.AspNetCore.Mvc;
using MyPatient.Web.Models;

namespace MyPatient.Web.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(patient);
        }
    }
}
