using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using MyPatient.Application.Services.MAServices;
using MyPatient.Application.Services.MedicalOrderServices;
using MyPatient.Application.Services.PatientServices;
using MyPatient.Models;
using MyPatient.Models.ViewModels.PatientVM;
using System.Drawing.Printing;

namespace MyPatient.Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IMAService _maService;
        private readonly IMedicalOrderService _medicalOrderService;

        public PatientController(IPatientService patientService, IMAService maService, IMedicalOrderService medicalOrderService)
        {
            _patientService = patientService;
            _maService = maService;
            _medicalOrderService = medicalOrderService;
        }

        public async Task<IActionResult> Index(int? page, string? filterSelected, string? filterCriteria)
        {
            IEnumerable<Patient> patientsList;
            var patientIndexVM = new PatientIndexVM();
            int pageSize = 10;

            ViewData["FilterSelected"] = filterSelected;
            ViewData["FilterCriteria"] = filterCriteria;

            if (!String.IsNullOrEmpty(filterCriteria))
            {
                switch (filterSelected)
                {
                    case "Name":
                        patientsList = _patientService.GetAllPatients(p => p.Name.Contains(filterCriteria), includeProperties: "MA");
                        break;

                    case "Record":
                        patientsList = _patientService.GetAllPatients(p => p.Record.Contains(filterCriteria), includeProperties: "MA");
                        break;

                    case "MA":
                        patientsList = _patientService.GetAllPatients(p => p.MA.FirstName.Contains(filterCriteria) || p.MA.LastName.Contains(filterCriteria), includeProperties: "MA");
                        break;

                    default:
                        patientsList = _patientService.GetAllPatients(p => true, includeProperties: "MA");
                        break;
                }
            }
            else
            {
                patientsList = _patientService.GetAllPatients(p => true, includeProperties: "MA");
            }

            patientIndexVM.Patients = await PaginatedList<Patient>.CreateAsync(patientsList.AsQueryable(), page ?? 1, pageSize);
            patientIndexVM.FilterOptions = new List<SelectListItem>
            {
                new SelectListItem{ Text = "Nombre", Value = "Name" },
                new SelectListItem{ Text = "Record", Value = "Record" },
                new SelectListItem{ Text = "MA", Value = "MA" }
            };
            
            return View(patientIndexVM);
        }

        public async Task<IActionResult> Upsert(long? id)
        {
            var patientVM = new PatientUpsertVM
            {
                Patient = new Patient(),
                MA = new MA(),
                MAs = _maService.PopulateMASelect()
            };

            if (id == null || id == 0)
            {
                return View(patientVM);
            }
            else
            {
                patientVM.Patient = await _patientService.GetPatient(p => p.Id == id);
                return View(patientVM);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(PatientUpsertVM patientVM)
        {
            if (ModelState.IsValid)
            {
                if(patientVM.Patient.Id == 0)
                {
                    await _patientService.AddPatient(patientVM.Patient);
                    TempData["success"] = "Paciente creado correctamente.";
                }
                else
                {
                    await _patientService.UpdatePatient(patientVM.Patient);
                    TempData["success"] = "Paciente actualizado correctamente.";
                }

                return RedirectToAction("Index");
            }
            else
            {
                patientVM.MAs = _maService.PopulateMASelect();
            }

            return View(patientVM);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var patient = await _patientService.GetPatient(p => p.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            var ma = await _maService.GetMA(m => m.Id == patient.MAId);

            if (ma == null)
            {
                return NotFound();
            }

            var patientVM = new PatientDeleteVM
            {
                Patient = patient,
                MA = String.Concat(ma.Sex ? "Dra. " : "Dr. ", " ", ma.FirstName, " ", ma.LastName)
            };

            return View(patientVM);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(long? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var patient = await _patientService.GetPatient(p => p.Id == id);

            if (patient is null)
            {
                return NotFound();
            }

            await _patientService.RemovePatient(patient);

            TempData["success"] = "Paciente eliminado correctamente.";

            return RedirectToAction("Index");
        }
    }
}
