using HospitalManagement.Common.Models;
using HospitalManagement.Controllers;
using HospitalManagement.Service.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace HospitalManagement.API.Tests
{
    public class PatientControllerTests
    {
        private readonly Mock<IPatientService> mockPatientService;
        private readonly Mock<ILogger<PatientsController>> mockLogger;
        private readonly PatientsController patientController;
        public PatientControllerTests() {
            this.mockPatientService = new Mock<IPatientService>();
            this.mockLogger = new Mock<ILogger<PatientsController>>();
            this.patientController = new PatientsController(this.mockLogger.Object, this.mockPatientService.Object);

        }

        [Fact]
        public async Task GetPatientsAsync_Should_Succeed_EmptyResult()
        {
            //Arrange
            this.mockPatientService.Setup(x => x.GetPatientsAsync()).ReturnsAsync(new List<Patient>());

            //Act
            var result = await this.patientController.GetPatients();

            //Assert
            var assertResult = Assert.IsAssignableFrom<IList<Patient>>(result);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public async Task GetPatientsAsync_Should_Succeed()
        {
            //Arrange
            var patients = new List<Patient>()
            {
                new Patient { Name = "Naveen", Age = 32 },
                new Patient { Name = "Kumar", Age = 24 }
            };
            this.mockPatientService.Setup(x => x.GetPatientsAsync()).ReturnsAsync(patients);

            //Act
            var result = await this.patientController.GetPatients();

            //Assert
            var assertResult = Assert.IsAssignableFrom<IList<Patient>>(result);
            Assert.Equal(2, result.Count);
        }
    }
}