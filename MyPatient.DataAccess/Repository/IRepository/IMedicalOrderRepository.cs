﻿using MyPatient.Models;
using MyPatient.Web.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPatient.DataAccess.Repository.IRepository
{
    public interface IMedicalOrderRepository : IBaseRepository<MedicalOrder>
    {
        Task<MedicalOrder> GetLast(TypeMedicalOrder type, long patientId);
    }
}
