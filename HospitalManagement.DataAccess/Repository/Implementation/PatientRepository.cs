using HospitalManagement.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            try
            {
                this._context.Patients.Add(patient);
                this._context.Entry(patient).State = EntityState.Added;
                await this._context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
