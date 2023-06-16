using AutoFixture;
using HospitalManagement.Common.Models;
using HospitalManagement.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.DataAccess.Tests
{
    public class PatientRepositoryTests
    {

        [Fact]
        public async void PatientRepository_GetPatientsAsync_Should_GetListOfPatients()
        {
            // Arrange

            var builder = new DbContextOptionsBuilder<PatientDBContext>();
            builder.UseInMemoryDatabase("GetPatientsAsync");
            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            using var context = new PatientDBContext(builder.Options);
            var patientrepository = new PatientRepository(context);

            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();
            await context.Patients.AddAsync(patient);
            await context.SaveChangesAsync();

            // Action
            var patients =await patientrepository.GetPatientsAsync();

            // Assert

            Assert.NotNull(patients);
            Assert.Equal(patient.Name , patients[0].Name);
            Assert.Equal(1, patients.Count);
        }

        [Fact]
        public async void PatientRepository_GetPatientAsync_Should_GetPatientById_Success()
        {
            // Arrange

            var builder = new DbContextOptionsBuilder<PatientDBContext>();
            builder.UseInMemoryDatabase("GetPatientByIdAsync");
            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            using var context = new PatientDBContext(builder.Options);
            var patientrepository = new PatientRepository(context);

            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();
            await context.Patients.AddAsync(patient);
            await context.SaveChangesAsync();

            // Action
            var patientOp = await patientrepository.GetPatientAsync(patient.Id);

            // Assert

            Assert.NotNull(patientOp);
            Assert.Equal(patient.Name, patientOp.Name);
        }

        [Fact]
        public async void PatientRepository_AddPatientAsync_Should_AddPatient_Success()
        {
            // Arrange

            var builder = new DbContextOptionsBuilder<PatientDBContext>();
            builder.UseInMemoryDatabase("AddPatientAsync");
            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            using var context = new PatientDBContext(builder.Options);
            var patientrepository = new PatientRepository(context);

            // Action
            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();
            await patientrepository.AddPatientAsync(patient);

            // Assert
            var patientOp = await patientrepository.GetPatientAsync(patient.Id);
            Assert.NotNull(patientOp);
            Assert.Equal(patient.Name, patientOp.Name);
        }

        [Fact]
        public async void PatientRepository_UpdatePatientAsync_Should_UpdatePatient_Success()
        {
            // Arrange

            var builder = new DbContextOptionsBuilder<PatientDBContext>();
            builder.UseInMemoryDatabase("UpdatePatientAsync");
            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            using var context = new PatientDBContext(builder.Options);
            var patientrepository = new PatientRepository(context);

            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();
            await context.Patients.AddAsync(patient);
            await context.SaveChangesAsync();

            // Action
            var patientOp = await patientrepository.UpdatePatientAsync(patient);

            // Assert

            Assert.True(patientOp);
        }

        [Fact]
        public async void PatientRepository_DeletePatientAsync_Should_DeletePatient_Success()
        {
            // Arrange

            var builder = new DbContextOptionsBuilder<PatientDBContext>();
            builder.UseInMemoryDatabase("DeletePatientAsync");
            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            using var context = new PatientDBContext(builder.Options);
            var patientrepository = new PatientRepository(context);

            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();
            await context.Patients.AddAsync(patient);
            await context.SaveChangesAsync();

            // Action
            var result =await patientrepository.DeletePatientAsync(patient.Id);

            // Assert
            Assert.True(result);
        }

    }
}