using HospitalManagement.Common.Models;

namespace HospitalManagement.Service.Services
{
    public class PatientService : IPatientService
    {
        /// <summary>
        /// GetPatientsAsync
        /// </summary>
        /// <returns>Task</returns>
        public async Task<IList<Patient>> GetPatientsAsync() {
            return await Task.FromResult(new List<Patient>());
        }

        /// <summary>
        /// GetPatientAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task</returns>
        public async Task<Patient> GetPatientAsync(int id){
            return await Task.FromResult(new Patient());
        }
    }
}
