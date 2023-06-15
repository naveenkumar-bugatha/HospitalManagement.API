using HospitalManagement.Common.Models;
using HospitalManagement.DataAccess.Repository;

namespace HospitalManagement.Service.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository) { 
            _patientRepository = patientRepository;
        }

        /// <summary>
        /// GetPatientsAsync
        /// </summary>
        /// <returns>Task</returns>
        public async Task<IList<Patient>> GetPatientsAsync() {
            return await _patientRepository.GetPatientsAsync();
        }

        /// <summary>
        /// GetPatientAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task</returns>
        public async Task<Patient> GetPatientAsync(int id){
            return await _patientRepository.GetPatientAsync(id);
        }

        // <summary>
        /// AddPatientAsync
        /// </summary>
        /// <param name="patient">patient</param>
        /// <returns>Task</returns>
        public async Task<bool> AddPatientAsync(Patient patient)
        {
            return await _patientRepository.AddPatientAsync(patient);
        }
    }
}
