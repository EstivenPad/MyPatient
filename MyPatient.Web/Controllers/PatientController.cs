using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPatient.Application.Services.MA;
using MyPatient.Application.Services.Patient;
using MyPatient.Models;
using MyPatient.Models.ViewModels;

namespace MyPatient.Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IMAService _maService;

        public PatientController(IPatientService patientService, IMAService maService)
        {
            _patientService = patientService;
            _maService = maService;
        }

        public async Task<IActionResult> Index()
        {
            var patients = await _patientService.GetAllPatients(x => true, includeProperties: "MA");

            var patientIndexVM = new PatientIndexVM
            {
                Patients = patients.ToList(),
                Filters = new List<SelectListItem>
                {
                    new SelectListItem{ Text = "Nombre", Value = "Name" },
                    new SelectListItem{ Text = "Record", Value = "Record" },
                    new SelectListItem{ Text = "MA", Value = "MA" }
                },
                SelectedFilter = "",
                FilterCriteria = ""
            };

            return View(patientIndexVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PatientIndexVM patientsVM)
        {
            IEnumerable<Patient> patientsList;

            if (!String.IsNullOrEmpty(patientsVM.FilterCriteria))
            {
                switch (patientsVM.SelectedFilter)
                {
                    case "Name":
                        patientsList = await _patientService.GetAllPatients(p => p.Name.Contains(patientsVM.FilterCriteria), includeProperties: "MA");
                        break;

                    case "Record":
                        patientsList = await _patientService.GetAllPatients(p => p.Record.Contains(patientsVM.FilterCriteria), includeProperties: "MA");
                        break;

                    case "MA":
                        patientsList = await _patientService.GetAllPatients(p => p.MA.FirstName.Contains(patientsVM.FilterCriteria), includeProperties: "MA");
                        break;

                    default:
                        patientsList = await _patientService.GetAllPatients(p => true, includeProperties: "MA");
                        break;
                }
            }
            else
            {
                patientsList = await _patientService.GetAllPatients(p => true, includeProperties: "MA");
            }

            var patientIndexVM = new PatientIndexVM
            {
                Patients = patientsList.ToList(),
                Filters = new List<SelectListItem>
                {
                    new SelectListItem{ Text = "Nombre", Value = "Name" },
                    new SelectListItem{ Text = "Record", Value = "Record" },
                    new SelectListItem{ Text = "MA", Value = "MA" }
                },
                SelectedFilter = "",
                FilterCriteria = ""
            };

            return View(patientIndexVM);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var patientVM = new PatientUpsertVM
            {
                Patient = new Patient(),
                MA = new MA(),
                MAs = await _maService.PopulateMASelect()
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
                patientVM.MAs = await _maService.PopulateMASelect();
            }

            return View(patientVM);
        }

        public async Task<IActionResult> Delete(int? id)
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
        public async Task<IActionResult> DeletePost(int? id)
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
