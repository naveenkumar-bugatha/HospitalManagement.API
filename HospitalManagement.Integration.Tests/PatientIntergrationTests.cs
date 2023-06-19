using HospitalManagement.Common.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using FluentAssertions;
using AutoFixture;
using Newtonsoft.Json;
using System.Text;

namespace HospitalManagement.Integration.Tests
{
    public class PatientIntergrationTests : WebApplicationFactory<Program>
    {
        private readonly WebApplicationFactory<Program> applicationFactory;
        private readonly HttpClient _client;

        public PatientIntergrationTests()
        {
            this.applicationFactory = new WebApplicationFactory<Program>();
            this._client = applicationFactory.CreateClient();
        }

        [Fact]
        public async Task Get_GetpatientsAsync_ShouldSuccess_WhenPatientsRetrieved()
        {
            // Act
            var response = await _client.GetAsync(HttpHelper.Urls.GetPatients);
            var result = await response.Content.ReadFromJsonAsync<List<Patient>>();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result?.Count().Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public async Task Get_GetpatientAsync_ShouldSuccess_WhenPatientreturnedbyId()
        {
            // Act
            var response = await _client.GetAsync(HttpHelper.Urls.GetPatient + "1");
            var result = await response.Content.ReadFromJsonAsync<Patient>();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result?.Id.Should().Be(1);
        }

        [Fact]
        public async Task Get_GetpatientAsync_ShouldReturn_BadRequest_WhenPassInvalidInput()
        {
            // Act
            var response = await _client.GetAsync(HttpHelper.Urls.GetPatient + "0");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task Post_AddPatientAsync_ShouldSuccess_WhenPatientGetsAdded()
        {
            // Arrange
            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");

            // Act
            await _client.PostAsync(HttpHelper.Urls.AddPatient, httpContent);

            // Assert
            var response = await _client.GetAsync(HttpHelper.Urls.GetPatient + patient.Id);
            var result = await response.Content.ReadFromJsonAsync<Patient>();
            result?.Id.Should().Be(patient.Id);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task Post_AddPatientAsync_Should_return_BadRequest_WhenWePass_InvalidInput()
        {
            // Arrange
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(HttpHelper.Urls.AddPatient, httpContent);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task Put_UpdatePatientAsync_Should_return_BadRequest_WhenWePass_InvalidInput()
        {
            // Arrange
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync(HttpHelper.Urls.UpdatePatient, httpContent);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_DeletePatientAsync_Should_return_BadRequest_WhenWePass_InvalidInput()
        {
            // Arrange
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.DeleteAsync(HttpHelper.Urls.DeletePatient + "0");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task Put_UpdatePatientAsync_ShouldSuccess_When_Patient_GetsUpdated()
        {
            // Arrange
            var fixture = new Fixture();
            var updatePatient = fixture.Create<Patient>();
            updatePatient.Id = 1;
            HttpContent httpContent2 = new StringContent(JsonConvert.SerializeObject(updatePatient), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync(HttpHelper.Urls.UpdatePatient, httpContent2);

            // Assert
            var getResponse = await _client.GetAsync(HttpHelper.Urls.GetPatient + updatePatient.Id);
            var result = await getResponse.Content.ReadFromJsonAsync<Patient>();
            result?.Id.Should().Be(1);
            result?.Name.Should().Be(updatePatient.Name);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_DeletePatientAsync_ShouldSuccess_WhenPatientGetsDeleted()
        {
            // Act
            var response = await _client.DeleteAsync(HttpHelper.Urls.DeletePatient + "2");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Should().NotBeNull();
        }
    }
}