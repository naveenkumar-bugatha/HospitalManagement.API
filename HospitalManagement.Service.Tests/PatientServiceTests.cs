using HospitalManagement.Common.Models;
using HospitalManagement.Service.Services;
using Xunit;
using Moq;
using HospitalManagement.DataAccess.Repository;
using AutoFixture;

namespace HospitalManagement.Service.Tests
{
    public class PatientServiceTests
    {
        private readonly Mock<IPatientRepository> mockPatientRepository;
        public PatientServiceTests()
        {
            this.mockPatientRepository = new Mock<IPatientRepository>();
        }

        [Fact]
        public async void PatienService_GetPatientsAsync_Should_Succeed()
        {
            //Arrange
            var fixture = new Fixture();
            var patients = fixture.Create<List<Patient>>();
            PatientService patientService = new PatientService(this.mockPatientRepository.Object);
            this.mockPatientRepository.Setup(x => x.GetPatientsAsync()).ReturnsAsync(patients);

            //Act
            var result = await patientService.GetPatientsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.IsAssignableFrom<IList<Patient>>(result);
        }

        [Fact]
        public async void PatienService_GetPatientAsync_Should_Succeed()
        {
            //Arrange
            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();
            PatientService patientService = new PatientService(this.mockPatientRepository.Object);
            this.mockPatientRepository.Setup(x => x.GetPatientAsync(It.IsAny<int>())).ReturnsAsync(patient);

            //Act
            var result = await patientService.GetPatientAsync(It.IsAny<int>());

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<Patient>(result);
        }

        [Fact]
        public async void PatienService_AddPatientAsync_Should_Succeed()
        {
            //Arrange
            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();
            PatientService patientService = new PatientService(this.mockPatientRepository.Object);
            this.mockPatientRepository.Setup(x => x.AddPatientAsync(It.IsAny<Patient>())).ReturnsAsync(true);

            //Act
            var result = await patientService.AddPatientAsync(It.IsAny<Patient>());

            //Assert
            Assert.True(result);
            Assert.IsAssignableFrom<bool>(result);
        }

        [Fact]
        public async void PatienService_DeletePatientAsync_Should_Succeed()
        {
            //Arrange
            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();
            PatientService patientService = new PatientService(this.mockPatientRepository.Object);
            this.mockPatientRepository.Setup(x => x.DeletePatientAsync(It.IsAny<int>())).ReturnsAsync(true);

            //Act
            var result = await patientService.DeletePatientAsync(It.IsAny<int>());

            //Assert
            Assert.True(result);
            Assert.IsAssignableFrom<bool>(result);
        }

        [Fact]
        public async void PatienService_UpdatePatientAsync_Should_Succeed()
        {
            //Arrange
            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();
            PatientService patientService = new PatientService(this.mockPatientRepository.Object);
            this.mockPatientRepository.Setup(x => x.UpdatePatientAsync(It.IsAny<Patient>())).ReturnsAsync(true);

            //Act
            var result = await patientService.UpdatePatientAsync(It.IsAny<Patient>());

            //Assert
            Assert.True(result);
            Assert.IsAssignableFrom<bool>(result);
        }
    }
}