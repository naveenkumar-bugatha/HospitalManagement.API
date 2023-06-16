using HospitalManagement.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.DataAccess.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientDBContext _context;

        public PatientRepository(PatientDBContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// GetPatientsAsync
        /// </summary>
        /// <returns>Task</returns>
        public async Task<IList<Patient>> GetPatientsAsync()
        {
            return await this._context.Patients.ToListAsync();
        }

        /// <summary>
        /// GetPatientAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task</returns>
        public async Task<Patient> GetPatientAsync(int id)
        {
            return await _context.Patients.FirstAsync(x => x.Id == id);
        }

        // <summary>
        /// AddPatientAsync
        /// </summary>
        /// <param name="patient">patient</param>
        /// <returns>Task</returns>
        public async Task<bool> AddPatientAsync(Patient patient)
        {
            this._context.Patients.Add(patient);
            this._context.Entry(patient).State = EntityState.Added;
            await this._context.SaveChangesAsync();
            return true;
        }

        // <summary>
        /// UpdatePatientAsync
        /// </summary>
        /// <param name="patient">patient</param>
        /// <returns>Task</returns>
        public async Task<bool> UpdatePatientAsync(Patient patient)
        {
            this._context.Patients.Update(patient);
            this._context.Entry(patient).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
            return true;
        }

        // <summary>
        /// DeletePatientAsync
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Task</returns>
        public async Task<bool> DeletePatientAsync(int id)
        {
            var localPatient = this._context.Set<Patient>().Local.
                FirstOrDefault(x => x.Id == id);
            if(localPatient != null)
            {
                this._context.Entry(localPatient).State = EntityState.Detached;
            }

            var patient = _context.Patients.First(x => x.Id == id);
            this._context.Patients.Remove(patient);
            this._context.Entry(patient).State = EntityState.Deleted;
            await this._context.SaveChangesAsync();
            return true;
        }
    }
}
