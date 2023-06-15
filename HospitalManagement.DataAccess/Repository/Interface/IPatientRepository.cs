using HospitalManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.DataAccess.Repository
{
    public interface IPatientRepository
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
    }
}
