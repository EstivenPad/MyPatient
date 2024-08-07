using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPatient.Application.Services.MAServices;
using MyPatient.Models;
using MyPatient.Models.ViewModels.PatientVM;

namespace MyPatient.Web.Controllers
{
    public class MAController : Controller
    {
        private readonly IMAService _maService;

        public MAController(IMAService maService)
        {
            _maService = maService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(MA ma)
        {
            if (ModelState.IsValid)
            {
                await _maService.AddMA(ma);
            }
            
            var patientVM = new PatientUpsertVM()
            {
                Patient = new Patient(),
                MA = new MA(),
            };

            var MAList = _maService.GetAllMAs(x => true);

            patientVM.MAs = MAList.OrderBy(ma => ma.FirstName).Select(ma => new SelectListItem
            {
                Text = String.Concat(ma.Sex ? "Dra. " : "Dr. ", " ", ma.FirstName, " ", ma.LastName),
                Value = ma.Id.ToString()
            });

            return View("Views/Patient/Upsert.cshtml", patientVM);

        }
    }
}
