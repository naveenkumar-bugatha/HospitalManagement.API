using HospitalManagement.Common.Models;
using HospitalManagement.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        private readonly IPatientService _patientService;

        public PatientController(ILogger<PatientController> logger, IPatientService patientService)
        {
            _logger = logger;
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IList<Patient>> GetPatients()
        {
            return await _patientService.GetPatientsAsync();
        }
    }
}