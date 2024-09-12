using Microsoft.AspNetCore.Mvc;
using MyPatient.Application.Services.DoctorServices;
using MyPatient.Application.Services.PatientServices;
using MyPatient.Application.Services.SurgicalProcedureServices;
using MyPatient.Models;
using MyPatient.Models.Enums;
using MyPatient.Models.ViewModels.MedicalOrderVM;
using MyPatient.Models.ViewModels.SurgicalProcedureVM;

namespace MyPatient.Web.Controllers
{
    public class SurgicalProcedureController : Controller
    {
        public readonly ISurgicalProcedureService _surgicalProcedureService;
        public readonly IDoctorService _doctorService;
        public readonly IPatientService _patientService;
        
        public SurgicalProcedureController(ISurgicalProcedureService surgicalProcedureService, IDoctorService doctorService, IPatientService patientService)
        {
            _surgicalProcedureService = surgicalProcedureService;
            _doctorService = doctorService;
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page, string? filterSelected, string? filterCriteria)
        {
            var pageSize = 10;

            var surgicalProcedureIndexVM = new SurgicalProcedureIndexVM();
            var surgicalProcedureList = _surgicalProcedureService.GetAllSurgicalProcedures(sp => true, includeProperties: "Patient,Patient.MA");
            surgicalProcedureIndexVM.SurgicalProcedures = await PaginatedList<SurgicalProcedure>.CreateAsync(surgicalProcedureList, page ?? 1, pageSize);
            
            ViewData["Title"] = "Proc. Quirurgicos";

            return View(surgicalProcedureIndexVM);
        }

        [HttpGet]
        public IActionResult PopulateDoctorDropList()
        {
            var doctorList = _doctorService.PopulateDoctorDroplist(TypeDoctor.Residente);

            return Json(doctorList);
        }

        [HttpGet]
        public async Task<IActionResult> Create(long? patientId = 1)
        {
            var patient = await _patientService.GetPatient(p => p.Id == patientId, "MA");
            
            var surgicalProcedureVM = new SurgicalProcedureVM()
            {
                SurgicalProcedure = new SurgicalProcedure(),
                NameMA = string.Concat(patient.MA.Sex ? "Dra. " : "Dr. ", patient.MA.FirstName, " ", patient.MA.LastName),
                ResidentDroplist = _doctorService.PopulateDoctorDroplist(TypeDoctor.Residente)
            };

            surgicalProcedureVM.SurgicalProcedure.Patient = patient;
            surgicalProcedureVM.SurgicalProcedure.PatientId = patient.Id;
            surgicalProcedureVM.SurgicalProcedure.Discoveries = new List<SurgicalProcedureDiscoveries> { new SurgicalProcedureDiscoveries() };
            surgicalProcedureVM.SurgicalProcedure.DoctorSurgicalProcedures = new List<Doctor_SurgicalProcedure> { new Doctor_SurgicalProcedure() };
            
            ViewData["Title"] = "Proc. Quirurgicos";

            return View(surgicalProcedureVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SurgicalProcedureVM surgicalProcedureVM)
        {
            if (ModelState.IsValid)
            {
                await _surgicalProcedureService.AddSurgicalProcedure(surgicalProcedureVM.SurgicalProcedure);
                
                TempData["Success"] = "Procedimiento Quirurgico creado correctamente.";
                
                return RedirectToAction("Index");
            }

            var patient = await _patientService.GetPatient(p => p.Id == surgicalProcedureVM.SurgicalProcedure.PatientId, "MA");

            surgicalProcedureVM.SurgicalProcedure.Patient = patient;
            surgicalProcedureVM.NameMA = string.Concat(patient.MA.Sex ? "Dra. " : "Dr. ", patient.MA.FirstName, " ", patient.MA.LastName);
            surgicalProcedureVM.ResidentDroplist = _doctorService.PopulateDoctorDroplist(TypeDoctor.Residente);

            ViewData["Title"] = "Proc. Quirurgicos";

            return View(surgicalProcedureVM);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(long surgicalProcedureId)
        {
            try
            {
                var surgicalProcedure = await _surgicalProcedureService.GetSurgicalProcedure(sp => sp.Id == surgicalProcedureId, includeProperties: "Patient,Patient.MA,Discoveries,DoctorSurgicalProcedures", asNoTracking: false);

                var surgicalProcedureVM = new SurgicalProcedureVM
                {
                    SurgicalProcedure = surgicalProcedure,
                    ResidentDroplist = _doctorService.PopulateDoctorDroplist(TypeDoctor.Residente),
                    NameMA = string.Concat(surgicalProcedure.Patient.MA.Sex ? "Dra. " : "Dr. ", surgicalProcedure.Patient.MA.FirstName, " ", surgicalProcedure.Patient.MA.LastName)
                };

                return View(surgicalProcedureVM);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SurgicalProcedureVM surgicalProcedureVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _surgicalProcedureService.UpdateSurgicalProcedure(surgicalProcedureVM.SurgicalProcedure);

                    TempData["Success"] = "Procedimiento Quirurgico actualizado correctamente.";

                    return RedirectToAction("Index");
                }

                var patient = await _patientService.GetPatient(p => p.Id == surgicalProcedureVM.SurgicalProcedure.PatientId, "MA");

                surgicalProcedureVM.SurgicalProcedure.Patient = patient;
                surgicalProcedureVM.NameMA = string.Concat(patient.MA.Sex ? "Dra. " : "Dr. ", patient.MA.FirstName, " ", patient.MA.LastName);
                surgicalProcedureVM.ResidentDroplist = _doctorService.PopulateDoctorDroplist(TypeDoctor.Residente);

                ViewData["Title"] = "Proc. Quirurgicos";

                return View(surgicalProcedureVM);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(long surgicalProcedureId)
        {
            try
            {
                var surgicalProcedure = await _surgicalProcedureService.GetSurgicalProcedure(sp => sp.Id == surgicalProcedureId, includeProperties: "Patient,Patient.MA,Discoveries,DoctorSurgicalProcedures", asNoTracking: false);

                var surgicalProcedureVM = new SurgicalProcedureVM
                {
                    SurgicalProcedure = surgicalProcedure,
                    ResidentDroplist = _doctorService.PopulateDoctorDroplist(TypeDoctor.Residente),
                    NameMA = string.Concat(surgicalProcedure.Patient.MA.Sex ? "Dra. " : "Dr. ", surgicalProcedure.Patient.MA.FirstName, " ", surgicalProcedure.Patient.MA.LastName)
                };

                return View(surgicalProcedureVM);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePost(long surgicalProcedureId)
        {
            try
            {
                var surgicalProcedure = await _surgicalProcedureService.GetSurgicalProcedure(mo => mo.Id == surgicalProcedureId, includeProperties: "Patient,Patient.MA,Discoveries,DoctorSurgicalProcedures", asNoTracking: false);

                if (surgicalProcedure is null)
                    return NotFound();

                await _surgicalProcedureService.RemoveSurgicalProcedure(surgicalProcedure);
                TempData["Success"] = "Procedimiento Quirurgico eliminado correctamente.";

                return Json(new { success = true });
            }
            catch
            {
                throw;
            }
        }
    }
}
