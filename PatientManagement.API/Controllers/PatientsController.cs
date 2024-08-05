using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientManagement.Application.DTOs;
using PatientManagement.Application.Services;
using PatientManagement.Application.Utils;

namespace PatientManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientAppService _patientAppService;

        public PatientsController(IPatientAppService patientAppService)
        {
            _patientAppService = patientAppService;
        }

        // POST: api/Patients
        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] PatientDto patientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdPatient = await _patientAppService.CreatePatientAsync(patientDto);
                return CreatedAtAction(nameof(GetPatientById), new { id = createdPatient.Id }, createdPatient);
            }
            catch (ApplicationException ex)
            {
                if (ex.InnerException is DuplicateException)
                {
                    return Conflict(ex.InnerException.Message);
                }
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Patients/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(Guid id, [FromBody] PatientDto patientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedPatient = await _patientAppService.UpdatePatientAsync(id, patientDto);
                return Ok(updatedPatient);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PATCH: api/Patients/Deactivate/{id}
        [HttpPatch("Deactivate/{id}")]
        public async Task<IActionResult> DeactivatePatient(Guid id, [FromBody] string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                return BadRequest("Deactivation reason must be provided.");
            }

            try
            {
                await _patientAppService.DeactivatePatientAsync(id, reason);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        // PATCH: api/Patients/Deactivate/{id}
        [HttpPatch("Activate/{id}")]
        public async Task<IActionResult> ActivatePatient(Guid id)
        {
            try
            {
                await _patientAppService.ActivatePatientAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<IActionResult> GetAllPatients(int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 10;

                var patients = await _patientAppService.GetAllAsync(pageNumber, pageSize);
                return Ok(patients);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        // GET: api/Patients/NoPaged
        [HttpGet("NoPaged")]
        public async Task<IActionResult> GetAllPatientsNoPaged()
        {
            try
            {
                var patients = await _patientAppService.GetAllNoPagedAsync();
                return Ok(patients);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Patients/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(Guid id)
        {
            try
            {
                var patient = await _patientAppService.GetPatientByIdAsync(id);
                return Ok(patient);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
