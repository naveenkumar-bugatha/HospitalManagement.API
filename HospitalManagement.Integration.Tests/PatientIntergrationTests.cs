using HospitalManagement.Common.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using FluentAssertions;
using AutoFixture;
using Newtonsoft.Json;
using System.Net.Http;
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
        public async Task GetpatientsAsync_ShouldReturnTheExpecedPatients()
        {
            // Arrange
            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            await _client.PostAsync(HttpHelper.Urls.AddPatient, httpContent);

            // Act
            var response = await _client.GetAsync(HttpHelper.Urls.GetPatients);
            var result = await response.Content.ReadFromJsonAsync<List<Patient>>();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result?.Count().Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public async Task GetpatientAsync_ShouldReturnTheExpecedPatient()
        {
            // Arrange
            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            await _client.PostAsync(HttpHelper.Urls.AddPatient, httpContent);

            // Act
            var response = await _client.GetAsync(HttpHelper.Urls.GetPatient + patient.Id);
            var result = await response.Content.ReadFromJsonAsync<Patient>();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result?.Id.Should().Be(patient.Id);
            result.Should()
                .BeEquivalentTo(patient,
                options => options.Excluding(t => t.Id));
        }

        [Fact]
        public async Task GetpatientAsync_ShouldReturn_BadRequestException()
        {
            // Act
            var response = await _client.GetAsync(HttpHelper.Urls.GetPatient + "0");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task AddPatientAsync_Should_AddPatient_Success()
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
        public async Task AddPatientAsync_Should_return_BadRequest()
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
        public async Task UpdatePatientAsync_Should_return_BadRequest()
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
        public async Task DeletePatientAsync_Should_return_BadRequest()
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
        public async Task UpdatePatientAsync_Should_UpdatePatient_Success()
        {
            // Arrange
            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();
            HttpContent httpContent1 = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            await _client.PostAsync(HttpHelper.Urls.AddPatient, httpContent1);

            var updatePatient = fixture.Create<Patient>();
            updatePatient.Id = patient.Id;
            HttpContent httpContent2 = new StringContent(JsonConvert.SerializeObject(updatePatient), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync(HttpHelper.Urls.UpdatePatient, httpContent2);

            // Assert
            var getResponse = await _client.GetAsync(HttpHelper.Urls.GetPatient + updatePatient.Id);
            var result = await getResponse.Content.ReadFromJsonAsync<Patient>();
            result?.Id.Should().Be(patient.Id);
            result?.Name.Should().Be(updatePatient.Name);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task DeletePatientAsync_Should_DeletePatient_Success()
        {
            // Arrange
            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
            await _client.PostAsync(HttpHelper.Urls.AddPatient, httpContent);

            // Act
            var response = await _client.DeleteAsync(HttpHelper.Urls.DeletePatient + patient.Id);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Should().NotBeNull();
        }
    }
}