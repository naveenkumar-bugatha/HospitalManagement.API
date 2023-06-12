using HospitalManagement.Common.Models;

namespace HospitalManagement.Service.Services
{
    public class PatientService : IPatientService
    {
        public async Task<IList<Patient>> GetPatientsAsync() {
            return await Task.FromResult(new List<Patient>());
        }
    }
}
