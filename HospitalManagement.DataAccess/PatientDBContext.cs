using HospitalManagement.Common.Models;
using Microsoft.EntityFrameworkCore;


namespace HospitalManagement.DataAccess
{
    public class PatientDBContext : DbContext
    {
        public PatientDBContext(DbContextOptions<PatientDBContext> options): base(options) { 
        }

        public DbSet<Patient> Patients { get; set; }
    }
}
