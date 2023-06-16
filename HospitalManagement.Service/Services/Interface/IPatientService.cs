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
        /// <param name="id">id</param>
        /// <returns>Task</returns>
        Task<Patient> GetPatientAsync(int id);

        /// <summary>
        /// AddPatientAsync
        /// </summary>
        /// <param name="patient">patient</param>
        /// <returns>Task</returns>
        Task<bool> AddPatientAsync(Patient patient);

        /// <summary>
        /// DeletePatientAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task</returns>
        Task<bool> DeletePatientAsync(int id);

        // <summary>
        /// UpdatePatientAsync
        /// </summary>
        /// <param name="patient">patient</param>
        /// <returns>Task</returns>
        Task<bool> UpdatePatientAsync(Patient patient);
    }
}
