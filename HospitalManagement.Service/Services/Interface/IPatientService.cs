using HospitalManagement.Common.Models;
using System.Collections;

namespace HospitalManagement.Service.Services
{
    public interface IPatientService
    {
        /// <summary>
        /// GetPatientsAsync
        /// </summary>
        /// <returns>Task</returns>
        Task<IList<Patient>> GetPatientsAsync();

        /// <summary>
        /// GetPatientAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task</returns>
        Task<Patient> GetPatientAsync(int id);
    }
}
