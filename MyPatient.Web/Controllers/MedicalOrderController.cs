using FastReport.Data;
using FastReport.Export.PdfSimple;
using FastReport.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPatient.Application.Services.MedicalOrderServices;
using MyPatient.Application.Services.PatientServices;
using MyPatient.Models;
using MyPatient.Models.ViewModels.MedicalOrderVM;
using MyPatient.Web.Models.Enums;
using System.Data;

namespace MyPatient.Web.Controllers
{
    public class MedicalOrderController : Controller
    {
        private readonly IMedicalOrderService _medicalOrderService;
        private readonly IPatientService _patientService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public MedicalOrderController(IMedicalOrderService medicalOrderService, IPatientService patientService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _medicalOrderService = medicalOrderService;
            _patientService = patientService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }
        
        public async Task<IActionResult> Index(long patientId) 
        {
            var medicalOrderVM = new MedicalOrderIndexVM();

            var patient = await _patientService.GetPatient(p => p.Id == patientId);

            if (patient is null)
                return NotFound();

            var medicalOrderList = _medicalOrderService.GetAllMedicalOrders(mo => mo.PatientId == patientId);

            var incomeMedicalOrderList = medicalOrderList
                .Where(mo => mo.Type == TypeMedicalOrder.Ingreso)
                .OrderByDescending(mo => mo.CreatedDate)
                .OrderByDescending(mo => mo.CreatedTime);

            var dailyMedicalOrderList = medicalOrderList
                .Where(mo => mo.Type == TypeMedicalOrder.Diaria)
                .OrderByDescending(mo => mo.CreatedDate)
                .OrderByDescending(mo => mo.CreatedTime);

            var postOperativeMedicalOrderList = medicalOrderList
                .Where(mo => mo.Type == TypeMedicalOrder.Postquirurgica)
                .OrderByDescending(mo => mo.CreatedDate)
                .OrderByDescending(mo => mo.CreatedTime);

            medicalOrderVM.Patient = patient;
            medicalOrderVM.Income = incomeMedicalOrderList;
            medicalOrderVM.Daily = dailyMedicalOrderList;
            medicalOrderVM.PostOperative = postOperativeMedicalOrderList;

            ViewData["Title"] = "Ordenes Médicas";

            return View(medicalOrderVM);
        }

        public IActionResult GenerateReport(long medicalOrderId, TypeMedicalOrder medicalOrderType, bool downloadPDF, long patientId = 0)
        {
            WebReport web = new WebReport();
            
            var path = $"{_webHostEnvironment.WebRootPath}\\Reports\\MedicalOrderReport.frx";
            web.Report.Load(path);

            var mssqlDataConnection = new MsSqlDataConnection();
            mssqlDataConnection.ConnectionString = _configuration.GetConnectionString("DefaultConnectionString");
            var Conn = mssqlDataConnection.ConnectionString;
            
            web.Report.SetParameterValue("Conn", Conn);
            web.Report.SetParameterValue("@Id", medicalOrderId);
            web.Report.SetParameterValue("@Type", (int)medicalOrderType);

            if (web.Report.Prepare() && !downloadPDF)
            {
                ViewData["PatientId"] = patientId;
                return View(web);
            }
            else
            {
                var pdfExport = new PDFSimpleExport();

                MemoryStream stream = new MemoryStream();
                web.Report.Export(pdfExport, stream);
                web.Report.Dispose();
                pdfExport.Dispose();
                stream.Position = 0;

                return File(stream, "application/pdf", "orden_medica.pdf");
            }
        }

        public async Task<IActionResult> CreateIncome(long patientId)
        {
            var patient = await _patientService.GetPatient(p => p.Id == patientId, includeProperties: "MA");

            if(patient is null || patient.MA is null)
            {
                return NotFound();
            }

            var medicalOrderVM = new MedicalOrderVM();

            medicalOrderVM.MedicalOrder = new MedicalOrder();
            medicalOrderVM.MedicalOrder.Solutions = new List<MedicalOrderDetail>();
            medicalOrderVM.MedicalOrder.Solutions.Add(new MedicalOrderDetail());

            medicalOrderVM.MedicalOrder.Patient = patient;
            medicalOrderVM.MedicalOrder.PatientId = patient.Id;

            medicalOrderVM.MedicalOrder.MAId = patient.MAId;
            medicalOrderVM.MedicalOrder.MA = patient.MA;
            medicalOrderVM.MA = string.Concat(patient.MA.Sex ? "Dra. " : "Dr. ", patient.MA.FirstName, " ", patient.MA.LastName);

            ViewData["Title"] = "Ordenes Médicas";

            return View("Create", medicalOrderVM);
        }

        public async Task<IActionResult> CreateDaily(long patientId, bool copyIncome)
        {
            MedicalOrder lastMedicalOrder;

            var patient = await _patientService.GetPatient(p => p.Id == patientId, includeProperties: "MA");

            if (patient is null || patient.MA is null)
                return NotFound();

            if (copyIncome)
                lastMedicalOrder = await _medicalOrderService.GetLastMedicalOrder(TypeMedicalOrder.Ingreso, patientId);
            else
                lastMedicalOrder = await _medicalOrderService.GetLastMedicalOrder(TypeMedicalOrder.Postquirurgica, patientId);
            
            if (lastMedicalOrder is null)
            {
                TempData["Warning"] = $"¡No existe ninguna Orden Médica {(copyIncome ? "de Ingreso" : "Post-quirúrgica")} creada!";
                return RedirectToAction("Index", new { patientId = patientId });
            }

            var medicalOrderVM = new MedicalOrderVM();

            medicalOrderVM.MedicalOrder = lastMedicalOrder;
            medicalOrderVM.MedicalOrder.Id = 0;
            medicalOrderVM.MedicalOrder.Type = TypeMedicalOrder.Diaria;
            medicalOrderVM.MedicalOrder.CreatedDate = DateOnly.FromDateTime(DateTime.Now);
            medicalOrderVM.MedicalOrder.CreatedTime = TimeOnly.FromDateTime(DateTime.Now);

            for (int i = 0; i < medicalOrderVM.MedicalOrder.Solutions?.Count(); i++)
            {
                medicalOrderVM.MedicalOrder.Solutions[i].MedicalOrderDetailId = 0;
            }

            medicalOrderVM.MA = string.Concat(patient.MA.Sex ? "Dra. " : "Dr. ", patient.MA.FirstName, " ", patient.MA.LastName);

            ViewData["Title"] = "Ordenes Médicas";

            return View("Create", medicalOrderVM);
        }

        public async Task<IActionResult> CreatePostOperative(long patientId)
        {
            var patient = await _patientService.GetPatient(p => p.Id == patientId, includeProperties: "MA");

            if (patient is null || patient.MA is null)
            {
                return NotFound();
            }

            var lastMedicalOrder = await _medicalOrderService.GetLastMedicalOrder(TypeMedicalOrder.Ingreso, patientId);
            
            if (lastMedicalOrder is null)
            {
                TempData["Warning"] = $"¡No existe ninguna Orden Médica de Ingreso creada!";
                return RedirectToAction("Index", new { patientId = patientId });
            }

            var medicalOrderVM = new MedicalOrderVM();
            medicalOrderVM.MedicalOrder = lastMedicalOrder;
            medicalOrderVM.MedicalOrder.Id = 0;
            medicalOrderVM.MedicalOrder.Type = TypeMedicalOrder.Postquirurgica;
            medicalOrderVM.MedicalOrder.CreatedDate = DateOnly.FromDateTime(DateTime.Now);
            medicalOrderVM.MedicalOrder.CreatedTime = TimeOnly.FromDateTime(DateTime.Now);

            for (int i = 0; i < medicalOrderVM.MedicalOrder.Solutions.Count(); i++)
            {
                medicalOrderVM.MedicalOrder.Solutions[i].MedicalOrderDetailId = 0;
            }

            medicalOrderVM.MA = string.Concat(patient.MA.Sex ? "Dra. " : "Dr. ", patient.MA.FirstName, " ", patient.MA.LastName);

            ViewData["Title"] = "Ordenes Médicas";

            return View("Create", medicalOrderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicalOrderVM medicalOrderVM)
        {
            if (ModelState.IsValid)
            {
                await _medicalOrderService.AddMedicalOrder(medicalOrderVM.MedicalOrder);

                TempData["Success"] = $"Orden Médica {Enum.GetName(medicalOrderVM.MedicalOrder.Type)} creada correctamente.";

                return RedirectToAction("Index", new { patientId = medicalOrderVM.MedicalOrder.PatientId });
            }

            var patient = await _patientService.GetPatient(p => p.Id == medicalOrderVM.MedicalOrder.PatientId, includeProperties: "MA");
                
            medicalOrderVM.MedicalOrder.Patient = patient;
            medicalOrderVM.MedicalOrder.PatientId = patient.Id;

            medicalOrderVM.MedicalOrder.MAId = patient.MAId;
            medicalOrderVM.MedicalOrder.MA = patient.MA;
            medicalOrderVM.MA = string.Concat(patient.MA.Sex ? "Dra. " : "Dr. ", patient.MA.FirstName, " ", patient.MA.LastName);

            ViewData["Title"] = "Ordenes Médicas";

            return View(medicalOrderVM);
        }

        public async Task<IActionResult> Edit(long patientId, long medicalOrderId)
        {
            var medicalOrder = await _medicalOrderService.GetMedicalOrder(mo => mo.Id == medicalOrderId, includeProperties: "Patient,MA,Solutions");

            if (medicalOrder is null || medicalOrder.Patient is null || medicalOrder.MA is null)
            {
                return NotFound();
            }

            var medicalOrderVM = new MedicalOrderVM();

            medicalOrderVM.MedicalOrder = medicalOrder;

            medicalOrderVM.MedicalOrder.Patient = medicalOrder.Patient;
            medicalOrderVM.MedicalOrder.PatientId = medicalOrder.PatientId;

            medicalOrderVM.MedicalOrder.MAId = medicalOrder.MAId;
            medicalOrderVM.MedicalOrder.MA = medicalOrder.MA;
            medicalOrderVM.MA = string.Concat(medicalOrder.MA.Sex ? "Dra. " : "Dr. ", medicalOrder.MA.FirstName, " ", medicalOrder.MA.LastName);

            ViewData["Title"] = "Ordenes Médicas";

            return View(medicalOrderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MedicalOrderVM medicalOrderVM)
        {
            if (ModelState.IsValid)
            {
                await _medicalOrderService.UpdateMedicalOrder(medicalOrderVM.MedicalOrder);
                
                TempData["Success"] = "Orden Médica actualizada correctamente.";
                
                return RedirectToAction("Index", new { patientId = medicalOrderVM.MedicalOrder.PatientId });
            }

            var patient = await _patientService.GetPatient(p => p.Id == medicalOrderVM.MedicalOrder.PatientId, includeProperties: "MA");

            medicalOrderVM.MedicalOrder.Patient = patient;
            medicalOrderVM.MedicalOrder.PatientId = patient.Id;

            medicalOrderVM.MedicalOrder.MAId = patient.MAId;
            medicalOrderVM.MedicalOrder.MA = patient.MA;
            medicalOrderVM.MA = string.Concat(patient.MA.Sex ? "Dra. " : "Dr. ", patient.MA.FirstName, " ", patient.MA.LastName);

            ViewData["Title"] = "Ordenes Médicas";

            return View(medicalOrderVM);
        }

        public async Task<IActionResult> Delete(long medicalOrderId) 
        {
            var medicalOrder = await _medicalOrderService.GetMedicalOrder(mo => mo.Id == medicalOrderId, includeProperties: "Patient,MA,Solutions");

            if (medicalOrder is null || medicalOrder.Patient is null || medicalOrder.MA is null)
            {
                return NotFound();
            }

            var medicalOrderVM = new MedicalOrderVM();

            medicalOrderVM.MedicalOrder = medicalOrder;

            medicalOrderVM.MedicalOrder.Patient = medicalOrder.Patient;
            medicalOrderVM.MedicalOrder.PatientId = medicalOrder.PatientId;

            medicalOrderVM.MedicalOrder.MAId = medicalOrder.MAId;
            medicalOrderVM.MedicalOrder.MA = medicalOrder.MA;
            medicalOrderVM.MA = string.Concat(medicalOrder.MA.Sex ? "Dra. " : "Dr. ", medicalOrder.MA.FirstName, " ", medicalOrder.MA.LastName);

            ViewData["Title"] = "Ordenes Médicas";

            return View("Delete", medicalOrderVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(long medicalOrderId)
        {
            var medicalOrder = await _medicalOrderService.GetMedicalOrder(mo => mo.Id == medicalOrderId, includeProperties: "Solutions");

            if(medicalOrder is null) 
            { 
                return NotFound();
            }

            await _medicalOrderService.RemoveMedicalOrder(medicalOrder);
            TempData["Success"] = "Orden Médica eliminada correctamente.";

            return Json(new { success = true });
        }
    }
}
