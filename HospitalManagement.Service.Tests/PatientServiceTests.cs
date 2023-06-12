using HospitalManagement.Common.Models;
using HospitalManagement.Service.Services;
using Xunit;

namespace HospitalManagement.Service.Tests
{
    public class PatientServiceTests
    {
        public PatientServiceTests()
        {

        }

        [Fact]
        public async void PatienService_Should_Succeed()
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
    }
}