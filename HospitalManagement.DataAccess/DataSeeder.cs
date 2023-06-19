using HospitalManagement.Common.Models;

namespace HospitalManagement.DataAccess
{
    public class DataSeeder
    {
        private readonly PatientDBContext _dbContext;

        public DataSeeder(PatientDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void SeedData()
        {
            if (!this._dbContext.Patients.Any())
            {
                var patients = new List<Patient>()
                {
                    new Patient() {
                        Name = "Raj",
                        Age = 25,
                        Address = "HYD",
                        Id = 1,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    },
                    new Patient() {
                        Name = "Kumar",
                        Age = 55,
                        Address = "CHN",
                        Id = 2,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    },
                };
                this._dbContext.Patients.AddRange(patients);
                this._dbContext.SaveChanges();
            }
        }
    }
}
