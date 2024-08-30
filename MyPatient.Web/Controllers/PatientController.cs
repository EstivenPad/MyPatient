using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPatient.Application.Services.DoctorServices;
using MyPatient.Application.Services.MedicalOrderServices;
using MyPatient.Application.Services.PatientServices;
using MyPatient.Models;
using MyPatient.Models.ViewModels.PatientVM;

namespace MyPatient.Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly IMedicalOrderService _medicalOrderService;

        public PatientController(IPatientService patientService, IDoctorService doctorService, IMedicalOrderService medicalOrderService)
        {
            _patientService = patientService;
            _doctorService = doctorService;
            _medicalOrderService = medicalOrderService;
        }

        public async Task<IActionResult> Index(int? page, string? filterSelected, string? filterCriteria)
        {
            IQueryable<Patient> patientsList;
            var patientIndexVM = new PatientIndexVM();
            int pageSize = 10;

            ViewData["FilterSelected"] = filterSelected;
            ViewData["FilterCriteria"] = filterCriteria;

            if (!String.IsNullOrWhiteSpace(filterCriteria))
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

            patientIndexVM.Patients = await PaginatedList<Patient>.CreateAsync(patientsList, page ?? 1, pageSize);
            patientIndexVM.FilterOptions = new List<SelectListItem>
            {
                new SelectListItem{ Text = "Nombre", Value = "Name" },
                new SelectListItem{ Text = "Record", Value = "Record" },
                new SelectListItem{ Text = "MA", Value = "MA" }
            };
            
            ViewData["Title"] = "Pacientes";

            return View(patientIndexVM);
        }

        public async Task<IActionResult> Upsert(long? id)
        {
            var patientVM = new PatientUpsertVM
            {
                Patient = new Patient(),
                MA = new Doctor(),
                MAs = _doctorService.PopulateMADroplist()
            };
            
            ViewData["Title"] = "Pacientes";

            if (id is not null)
            {
                patientVM.Patient = await _patientService.GetPatient(p => p.Id == id);
                patientVM.MA = await _doctorService.GetDoctor(d => d.Id == patientVM.Patient.MAId);
            }

            return View(patientVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(PatientUpsertVM patientVM)
        {
            if (ModelState.IsValid)
            {
                if(patientVM.Patient.Id == 0)
                {
                    await _patientService.AddPatient(patientVM.Patient);
                    TempData["Success"] = "Paciente creado correctamente.";
                }
                else
                {
                    await _patientService.UpdatePatient(patientVM.Patient);
                    TempData["Success"] = "Paciente actualizado correctamente.";
                }

                return RedirectToAction("Index");
            }
            else
            {
                patientVM.MAs = _doctorService.PopulateMADroplist();
            }

            ViewData["Title"] = "Pacientes";

            return View(patientVM);
        }

        public async Task<IActionResult> Delete(long id)
        {
            var patient = await _patientService.GetPatient(p => p.Id == id);

            if (patient is null)
            {
                return NotFound();
            }

            var doctor = await _doctorService.GetDoctor(m => m.Id == patient.MAId);

            if (doctor is null)
            {
                return NotFound();
            }

            var patientVM = new PatientDeleteVM
            {
                Patient = patient,
                MA = String.Concat(doctor.Sex ? "Dra. " : "Dr. ", " ", doctor.FirstName, " ", doctor.LastName)
            };

            ViewData["Title"] = "Pacientes";

            return View(patientVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(long id)
        {
            var patient = await _patientService.GetPatient(p => p.Id == id);

            if (patient is null)
            {
                return NotFound();
            }

            var patientHasMedicalOrder = await _patientService.HasMedicalOrders(id);

            if (patientHasMedicalOrder)
            {
                TempData["Danger"] = "¡No se puede eliminar el Paciente debido a que tiene Ordenes Medicas asignadas!";

                return Json(new { success = false, redirectUrl = $"/Patient/Delete/{id}" });
            }

            await _patientService.RemovePatient(patient);

            TempData["Success"] = "Paciente eliminado correctamente.";

            return Json(new { success = true });
        }
    }
}
