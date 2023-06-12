using HospitalManagement.Common.Models;
using System.Collections;

namespace HospitalManagement.Service.Services
{
    public interface IPatientService
    {
        Task<IList<Patient>> GetPatientsAsync();
    }
}
