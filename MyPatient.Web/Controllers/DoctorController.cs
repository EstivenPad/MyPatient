using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPatient.Application.Services.DoctorServices;
using MyPatient.Models;
using MyPatient.Models.ViewModels.PatientVM;
using MyPatient.Models.Enums;
using MyPatient.Models.ViewModels.DoctorVM;
using FluentValidation;
using MyPatient.Application.Validations;
using FluentValidation.Results;

namespace MyPatient.Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IValidator<Doctor> _validator;

        public DoctorController(IDoctorService doctorService, IValidator<Doctor> validator)
        {
            _doctorService = doctorService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page, string? filterSelected, string? filterCriteria, string? medicalLevelOption)
        {
            IQueryable<Doctor> doctorList;
            var doctorIndexVM = new DoctorIndexVM();
            int pageSize = 10;

            ViewData["FilterSelected"] = filterSelected;
            ViewData["FilterCriteria"] = filterCriteria;
            ViewData["MedicalLevelOption"] = medicalLevelOption;

            switch (medicalLevelOption)
            {
                case "MA":
                    doctorList = _doctorService.GetAllDoctors(d => d.Type == TypeDoctor.MA);
                    break;

                case "Resident":
                    doctorList = _doctorService.GetAllDoctors(d => d.Type == TypeDoctor.Residente);
                    break;

                default:
                    doctorList = _doctorService.GetAllDoctors(d => true);
                    break;
            }

            if (!String.IsNullOrWhiteSpace(filterCriteria))
            {
                switch (filterSelected)
                {
                    case "Name":
                        doctorList = doctorList.Where(d => d.FirstName.Contains(filterCriteria) || d.LastName.Contains(filterCriteria));
                        break;

                    case "Exequatur":
                        doctorList = doctorList.Where(d => d.Exequatur.Contains(filterCriteria));
                        break;
                }
            }

            doctorIndexVM.Doctors = await PaginatedList<Doctor>.CreateAsync(doctorList, page ?? 1, pageSize);
            doctorIndexVM.FilterOptions = new List<SelectListItem>
            {
                new SelectListItem{ Text = "Nombre", Value = "Name" },
                new SelectListItem{ Text = "Exequatur", Value = "Exequatur" }
            };

            ViewData["Title"] = "Doctores";

            //Check if the request come from ajax or fetch for display index as a partial view
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(doctorIndexVM);

            return View(doctorIndexVM);
        }

        //*********MODAL CREATE MA IN PATIENT FORM************
        [HttpPost]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            doctor.Type = TypeDoctor.MA;

            if (ModelState.IsValid)
                await _doctorService.AddDoctor(doctor);

            var patientVM = new PatientUpsertVM()
            {
                Patient = new Patient(),
                MA = new Doctor(),
                MADropList = _doctorService.PopulateDoctorDroplist(TypeDoctor.MA)
            };

            patientVM.MA.Type = TypeDoctor.MA;

            return View("Views/Patient/Upsert.cshtml", patientVM);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var doctor = new Doctor();

            if(id is not null)
                doctor = await _doctorService.GetDoctor(d => d.Id == id);

            ViewData["Title"] = "Doctores";

            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Doctor doctor)
        {
            if (doctor.Id != 0)
            {
                var validationResult = await _validator.ValidateAsync(doctor);
                
                if(!validationResult.IsValid)
                    validationResult.AddToModelState(this.ModelState);
            }

            if (!ModelState.IsValid)
                return View(doctor);

            if (doctor.Id == 0)
            {
                await _doctorService.AddDoctor(doctor);
                TempData["Success"] = "Doctor creado correctamente.";
            }
            else
            {
                await _doctorService.UpdateDoctor(doctor);
                TempData["Success"] = "Doctor actualizado correctamente.";
            }

            ViewData["Title"] = "Doctores";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _doctorService.GetDoctor(d => d.Id == id);

            if (doctor is null)
                return NotFound();

            ViewData["Title"] = "Doctores";

            return View(doctor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var doctor = await _doctorService.GetDoctor(d => d.Id == id);

            if (doctor is null)
                return NotFound();
            
            bool hasPatients = await _doctorService.HasPatients(id);

            if (hasPatients)
            {
                TempData["Danger"] = "¡No se puede eliminar el Doctor debido a que tiene Pacientes asignados!";
                return Json(new { success = false, redirectUrl = $"/Doctor/Delete/{id}" });
            }

            await _doctorService.RemoveDoctor(doctor);
            TempData["Success"] = "Doctor eliminado correctamente.";

            return Json(new { success = true });
        }
    }
}
