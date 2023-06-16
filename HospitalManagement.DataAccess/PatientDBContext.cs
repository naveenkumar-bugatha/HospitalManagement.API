using HospitalManagement.Common.Models;
using Microsoft.EntityFrameworkCore;


namespace HospitalManagement.DataAccess
{
    public class PatientDBContext : DbContext
    {
        public PatientDBContext(DbContextOptions<PatientDBContext> options): base(options) { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "PatientDB");
        }
        public DbSet<Patient> Patients { get; set; }

    }
}
