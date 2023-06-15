using HospitalManagement.Common.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using FluentAssertions;
using AutoFixture;
using Newtonsoft.Json;

namespace HospitalManagement.Integration.Tests
{
    public class PatientIntergrationTests : WebApplicationFactory<Patient>
    {
        private readonly HttpClient _client;

        public PatientIntergrationTests()
        {
            this._client = new HttpClient();
        }

        [Fact]
        public async Task GetpatientsAsync_ShouldReturnTheExpecedPatients()
        {
            // Act
            var response = await _client.GetAsync(HttpHelper.Urls.GetPatients);
            var result = await response.Content.ReadFromJsonAsync<List<Patient>>();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result?.Count().Should().BeGreaterThan(1);
            result.Should()
                .BeEquivalentTo(new List<Patient>(),
                options => options.Excluding(t => t.Id));
        }

        [Fact]
        public async Task GetpatientAsync_ShouldReturnTheExpecedPatient()
        {
            // Act
            var response = await _client.GetAsync(HttpHelper.Urls.GetPatient + "1");
            var result = await response.Content.ReadFromJsonAsync<Patient>();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result?.Id.Should().Be(1);
            result.Should()
                .BeEquivalentTo(new Patient(),
                options => options.Excluding(t => t.Id));
        }

        [Fact]
        public async Task GetpatientAsync_ShouldReturn_BadRequestException()
        {
            // Act
            var response = await _client.GetAsync(HttpHelper.Urls.GetPatient + "0");
            var result = await response.Content.ReadFromJsonAsync<Patient>();

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            result?.Should().BeNull();
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task AddPatientAsync_ShouldReturn_AddPatient_Success()
        {
            // Arrange
            var fixture = new Fixture();
            var patient = fixture.Create<Patient>();
            HttpContent httpContent = new StringContent(JsonConvert.ToString(patient));

            // Act
            var response = await _client.PostAsync(HttpHelper.Urls.AddPatient, httpContent);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Should().NotBeNull();
        }
    }
}