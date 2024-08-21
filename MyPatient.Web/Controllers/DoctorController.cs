using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPatient.Application.Services.DoctorServices;
using MyPatient.Models;
using MyPatient.Models.ViewModels.PatientVM;
using MyPatient.Models.Enums;
using MyPatient.Models.ViewModels.DoctorVM;
using Azure.Core;

namespace MyPatient.Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
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

            if (Request.Headers != null && Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(doctorIndexVM);

            return View(doctorIndexVM);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                await _doctorService.AddDoctor(doctor);
            }
            
            var patientVM = new PatientUpsertVM()
            {
                Patient = new Patient(),
                MA = new Doctor(),
            };

            var MAList = _doctorService.GetAllDoctors(d => d.Type == TypeDoctor.MA);

            patientVM.MAs = MAList.OrderBy(ma => ma.FirstName).Select(ma => new SelectListItem
            {
                Text = String.Concat(ma.Sex ? "Dra. " : "Dr. ", " ", ma.FirstName, " ", ma.LastName),
                Value = ma.Id.ToString()
            });

            return View("Views/Patient/Upsert.cshtml", patientVM);

        }
    }
}
