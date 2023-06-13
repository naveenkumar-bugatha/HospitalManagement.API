using HospitalManagement.Common.Models;
using HospitalManagement.Service.Services;
using Xunit;
using Moq;

namespace HospitalManagement.Service.Tests
{
    public class PatientServiceTests
    {
        public PatientServiceTests()
        {

        }

        [Fact]
        public async void PatienService_GetPatientsAsync_Should_Succeed()
        {
            //Arrange
            PatientService patientService = new PatientService();

            //Act
            var result = await patientService.GetPatientsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
            Assert.IsAssignableFrom<IList<Patient>>(result);
        }

        [Fact]
        public async void PatienService_GetPatientAsync_Should_Succeed()
        {
            //Arrange
            PatientService patientService = new PatientService();

            //Act
            var result = await patientService.GetPatientAsync(It.IsAny<int>());

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<Patient>(result);
        }
    }
}