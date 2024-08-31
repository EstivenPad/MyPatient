using FluentValidation;
using MyPatient.Application.Services.DoctorServices;
using MyPatient.Models;
using MyPatient.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.Application.Validations.DoctorValidations
{
    public class UpdateDoctorValidator : AbstractValidator<Doctor>
    {
        private readonly IDoctorService _doctorService;

        public UpdateDoctorValidator(IDoctorService doctorService)
        {
            RuleFor(d => d.Type)
                .NotNull()
                .MustAsync(MACanDowngradeToResident)
                .WithMessage("No puede modificar el nivel médico porque tiene pacientes asignados.");

            _doctorService = doctorService;
        }

        private async Task<bool> MACanDowngradeToResident(Doctor doctor, TypeDoctor type, CancellationToken token)
        {
            var doctorDB = await _doctorService.GetDoctor(d => d.Id == doctor.Id);
            bool hasPatients = await _doctorService.HasPatients(doctor.Id);

            //Checking if the doctor had the medical level 'MA' before the update
            if(doctorDB.Type != type)
                return (!hasPatients);
            else
                return true;
        }
    }
}
