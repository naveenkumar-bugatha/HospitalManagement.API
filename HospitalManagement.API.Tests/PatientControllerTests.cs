using HospitalManagement.Common.Models;
using HospitalManagement.Controllers;
using HospitalManagement.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
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
        public async Task GetPatientsAsync_Should_Return_EmptyResult_404()
        {
            //Arrange
            this.mockPatientService.Setup(x => x.GetPatientsAsync()).ReturnsAsync((IList<Patient>)null);

            //Act
            var result = await this.patientController.GetPatients().ConfigureAwait(false);
            var objectResult = (NotFoundObjectResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetPatientsAsync_Should_Succeed_200()
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
            var objectResult = (OkObjectResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetPatientAsyncById_Should_Return_EmptyResult_404()
        {
            //Arrange
            this.mockPatientService.Setup(x => x.GetPatientAsync(It.IsAny<int>())).ReturnsAsync((Patient)null);

            //Act
            var result = await this.patientController.GetPatient(1).ConfigureAwait(false);
            var objectResult = (NotFoundObjectResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetPatientAsyncById_Should_Succeed_200()
        {
            //Arrange
            var patient = new Patient { Name = "Naveen", Age = 32 };

            this.mockPatientService.Setup(x => x.GetPatientAsync(It.IsAny<int>())).ReturnsAsync(patient);

            //Act
            var result = await this.patientController.GetPatient(1);
            var objectResult = (OkObjectResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetPatientAsyncById_Should_Return_badrequest_400()
        {
            //Act
            var result = await this.patientController.GetPatient(0);
            var objectResult = (BadRequestObjectResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        }
    }
}