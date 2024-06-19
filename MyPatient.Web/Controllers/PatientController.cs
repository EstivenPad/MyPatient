using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPatient.Application.Services.Patient;
using MyPatient.Models;

namespace MyPatient.Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _patientService.GetAllPatients());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string filterOption, string filterCriteria)
        {
            ViewData["FilterCriteria"] = filterCriteria;

            return View(await _patientService.GetFilteredPatients(filterOption, filterCriteria));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                await _patientService.AddPatient(patient);

                TempData["success"] = "Paciente creado correctamente.";

                return RedirectToAction("Index");
            }

            return View(patient);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Patient patient = await _patientService.GetPatient(p => p.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                await _patientService.UpdatePatient(patient);

                TempData["success"] = "Paciente actualizado correctamente.";

                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Patient patient = await _patientService.GetPatient(p => p.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Patient? patient = await _patientService.GetPatient(p => p.Id == id);

            if (patient is null)
                return NotFound();

            await _patientService.RemovePatient(patient);

            TempData["success"] = "Paciente eliminado correctamente.";

            return RedirectToAction("Index");
        }
    }
}
