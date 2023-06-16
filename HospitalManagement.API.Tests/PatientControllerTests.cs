using HospitalManagement.Common.Models;
using HospitalManagement.Controllers;
using HospitalManagement.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using AutoFixture;
using FluentAssertions;
using Xunit;
using System.Web.Http;

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
            var objectResult = (NotFoundResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
            mockPatientService.Verify(x => x.GetPatientsAsync(), Times.Once());
        }

        [Fact]
        public async Task GetPatientsAsync_Should_Succeed_200()
        {
            //Arrange
            var fixture = new Fixture();
            var patients = fixture.Create<List<Patient>>();
            this.mockPatientService.Setup(x => x.GetPatientsAsync()).ReturnsAsync(patients);

            //Act
            var result = await this.patientController.GetPatients();
            var objectResult = (OkObjectResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
            mockPatientService.Verify(x => x.GetPatientsAsync(), Times.Once());
        }

        [Fact]
        public async Task GetPatientAsyncById_Should_Return_EmptyResult_404()
        {
            //Arrange
            this.mockPatientService.Setup(x => x.GetPatientAsync(It.IsAny<int>())).ReturnsAsync((Patient)null);

            //Act
            var result = await this.patientController.GetPatient(1).ConfigureAwait(false);
            var objectResult = (NotFoundResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
            mockPatientService.Verify(x => x.GetPatientsAsync(), Times.Never());
        }

        [Fact]
        public async Task GetPatientAsyncById_Should_Succeed_200()
        {
            //Arrange
            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();

            this.mockPatientService.Setup(x => x.GetPatientAsync(It.IsAny<int>())).ReturnsAsync(patient);

            //Act
            var result = await this.patientController.GetPatient(1);
            var objectResult = (OkObjectResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
            mockPatientService.Verify(x => x.GetPatientAsync(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task GetPatientAsyncById_Should_Return_badrequest_400()
        {
            //Act
            var result = await this.patientController.GetPatient(0);
            var objectResult = (BadRequestResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
            mockPatientService.Verify(x => x.GetPatientsAsync(), Times.Never());
        }

        [Fact]
        public async Task DeletePatientAsync_Should_Return_Success_200()
        {
            // Assert
            this.mockPatientService.Setup(x => x.DeletePatientAsync(It.IsAny<int>())).ReturnsAsync(true);
            
            //Act
            var result = await this.patientController.DeletePatientAsync(1);
            var objectResult = (OkResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<OkResult>(result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
            mockPatientService.Verify(x => x.DeletePatientAsync(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task DeletePatientAsync_Should_Return_InternalServer_500()
        {
            // Assert
            this.mockPatientService.Setup(x => x.DeletePatientAsync(It.IsAny<int>())).ReturnsAsync(false);

            //Act
            var result = await this.patientController.DeletePatientAsync(1);
            var objectResult = (InternalServerErrorResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<InternalServerErrorResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            mockPatientService.Verify(x => x.DeletePatientAsync(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task DeletePatientAsync_Should_Return_BadRequest_400()
        {
            //Act
            var result = await this.patientController.DeletePatientAsync(0);
            var objectResult = (BadRequestResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
            mockPatientService.Verify(x => x.GetPatientsAsync(), Times.Never());
        }

        [Fact]
        public async Task AddPatientAsync_Should_Succeed_200()
        {
            //Arrange
            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();

            this.mockPatientService.Setup(x => x.AddPatientAsync(It.IsAny<Patient>())).ReturnsAsync(true);

            //Act
            var result = await this.patientController.AddPatientAsync(patient);
            var objectResult = (OkResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<OkResult>(result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
            mockPatientService.Verify(x => x.AddPatientAsync(It.IsAny<Patient>()), Times.Once());
        }

        [Fact]
        public async Task AddPatientAsync_Should_BadRequest_400()
        {

            //Act
            var result = await this.patientController.AddPatientAsync(null);
            var objectResult = (BadRequestResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
            mockPatientService.Verify(x => x.AddPatientAsync(It.IsAny<Patient>()), Times.Never());
        }

        [Fact]
        public async Task AddPatientAsync_Should_InternalServer_500()
        {
            //Arrange
            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();

            this.mockPatientService.Setup(x => x.AddPatientAsync(It.IsAny<Patient>())).ReturnsAsync(false);

            //Act
            var result = await this.patientController.AddPatientAsync(patient);
            var objectResult = (InternalServerErrorResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<InternalServerErrorResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            mockPatientService.Verify(x => x.AddPatientAsync(It.IsAny<Patient>()), Times.Once());
        }

        [Fact]
        public async Task UpdatePatientAsync_Should_Succeed_200()
        {
            //Arrange
            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();

            this.mockPatientService.Setup(x => x.UpdatePatientAsync(It.IsAny<Patient>())).ReturnsAsync(true);

            //Act
            var result = await this.patientController.UpdatePatientAsync(patient);
            var objectResult = (OkResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<OkResult>(result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
            mockPatientService.Verify(x => x.UpdatePatientAsync(It.IsAny<Patient>()), Times.Once());
        }

        [Fact]
        public async Task UpdatePatientAsync_Should_BadRequest_400()
        {

            //Act
            var result = await this.patientController.UpdatePatientAsync(null);
            var objectResult = (BadRequestResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
            mockPatientService.Verify(x => x.UpdatePatientAsync(It.IsAny<Patient>()), Times.Never());
        }

        [Fact]
        public async Task UpdatePatientAsync_Should_InternalServer_500()
        {
            //Arrange
            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();

            this.mockPatientService.Setup(x => x.UpdatePatientAsync(It.IsAny<Patient>())).ReturnsAsync(false);

            //Act
            var result = await this.patientController.UpdatePatientAsync(patient);
            var objectResult = (InternalServerErrorResult)result;

            //Assert
            Assert.NotNull(result);
            var assertResult = Assert.IsAssignableFrom<InternalServerErrorResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            mockPatientService.Verify(x => x.UpdatePatientAsync(It.IsAny<Patient>()), Times.Once());
        }


    }
}