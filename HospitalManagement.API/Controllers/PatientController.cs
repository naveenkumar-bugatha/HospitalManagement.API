using HospitalManagement.Common.Models;
using HospitalManagement.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace HospitalManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;
        private readonly IPatientService _patientService;

        /// <summary>
        /// PatientsController.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="patientService">patientService.</param>
        public PatientsController(ILogger<PatientsController> logger, IPatientService patientService)
        {
            _logger = logger;
            _patientService = patientService;
        }

        /// <summary>
        /// GetPatients.
        /// </summary>
        /// <returns>Task</returns>
        [HttpGet("GetPatients")]
        public async Task<IActionResult> GetPatients()
        {
            var result = await _patientService.GetPatientsAsync();
            if(result == null){
                return new NotFoundResult();
            }
            else {
                return new OkObjectResult(result);
            }
        }

        /// <summary>
        /// Get patient by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetPatient/{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            if(id != 0)
            {
                var result = await _patientService.GetPatientAsync(id);
                if (result == null)
                {
                    return new NotFoundResult();
                }
                else
                {
                    return new OkObjectResult(result);
                }
            }
            else
            {
                return new BadRequestResult();
            }
            
        }

        /// <summary>
        /// AddPatientAsync.
        /// </summary>
        /// <param name="patient">Patient.</param>
        /// <returns></returns>
        [HttpPost("AddPatient")]
        public async Task<IActionResult> AddPatientAsync(Patient patient)
        {
            if (!ModelState.IsValid || patient == null)
            {
                return new BadRequestResult();
            }

            if (await _patientService.AddPatientAsync(patient))
            {
                return Ok();
            }

            return new InternalServerErrorResult();
        }

        /// <summary>
        /// UpdatePatientAsync.
        /// </summary>
        /// <param name="patient">Patient.</param>
        /// <returns></returns>
        [HttpPut("UpdatePatient")]
        public async Task<IActionResult> UpdatePatientAsync(Patient patient)
        {
            if (!ModelState.IsValid || patient == null)
            {
                return new BadRequestResult();
            }

            if (await _patientService.UpdatePatientAsync(patient))
            {
                return Ok();
            }

            return new InternalServerErrorResult();
        }

        /// <summary>
        /// DeletePatientAsync.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns></returns>
        [HttpPut("DeletePatient")]
        public async Task<IActionResult> DeletePatientAsync(int id)
        {
            if (id == 0)
            {
                return new BadRequestResult();
            }

            if (await _patientService.DeletePatientAsync(id))
            {
                return Ok();
            }

            return new InternalServerErrorResult();
        }
    }
}