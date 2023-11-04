using API.Dtos;
using API.Entities;
using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace API.Controllers
{
    public class DoctorController : BaseController
    {

        private readonly IDoctorService _doctorService;
        private readonly IUserProviderService _currentUserService;

        public DoctorController(IDoctorService doctorService, IUserProviderService currentUserService)
        {
            _doctorService = doctorService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            var result = await _doctorService.GetAllDoctors();
            return Ok(result);
        }

        [HttpGet("doctorsByClinic/{clinicId}")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorsByClinicId(Guid clinicId)
        {
            var result = await _doctorService.GetDoctorsByClinicId(clinicId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctor(Guid id)
        {
            var result = await _doctorService.GetDoctor(id);
            return Ok(result);
        }

        [HttpPut("{doctorId}")]
        public async Task<IActionResult> UpdateDoctor(Guid doctorId, [FromBody] DoctorUpdateDto doctorUpdateDto)
        {
            await _doctorService.UpdateDoctor(doctorId, doctorUpdateDto);

            return NoContent();
        }
        [HttpPost("admission")]
        public async Task<IActionResult> AddAdmissionCondition( [FromBody] DoctorAdmissionConditionsDto? admissionConditionDto)
        {
            var doctorId = _currentUserService.GetCurrentUserId();

            var admissionCondition = new DoctorAdmissionConditions
            {
                Specialization = admissionConditionDto.Specialization,
                IsNFZ = admissionConditionDto.IsNFZ,
                IsPrivate = admissionConditionDto.IsPrivate,
                ConsultationFee = admissionConditionDto.ConsultationFee,
                WorkingDays = admissionConditionDto.WorkingDays,
                WorkHoursStart = admissionConditionDto.WorkHoursStart,
                WorkHoursEnd = admissionConditionDto.WorkHoursEnd,
                ClinicId = admissionConditionDto.ClinicId,
                DoctorId = Guid.Parse(doctorId)
            };

            await _doctorService.AddAdmissionConditionToDoctor(admissionCondition);
            return NoContent();
        }




        [HttpDelete("{doctorId}/admissionconditions/{admissionConditionId}")]
        public async Task<IActionResult> DeleteAdmissionCondition(Guid doctorId, Guid admissionConditionId)
        {
            await _doctorService.DeleteAdmissionConditionForDoctor(doctorId, admissionConditionId);
            return NoContent();
        }

        [HttpPut("{doctorId}/admissionconditions/{admissionConditionId}")]
        public async Task<IActionResult> UpdateAdmissionCondition(Guid doctorId, Guid admissionConditionId, [FromBody] DoctorAdmissionConditionsDto updatedAdmissionConditionDto)
        {
            var updatedAdmissionCondition = new DoctorAdmissionConditions
            {
                Specialization = updatedAdmissionConditionDto.Specialization,
                IsNFZ = updatedAdmissionConditionDto.IsNFZ,
                IsPrivate = updatedAdmissionConditionDto.IsPrivate,
                ConsultationFee = updatedAdmissionConditionDto.ConsultationFee,
                WorkingDays = updatedAdmissionConditionDto.WorkingDays,
                WorkHoursStart = updatedAdmissionConditionDto.WorkHoursStart,
                WorkHoursEnd = updatedAdmissionConditionDto.WorkHoursEnd
            };

            await _doctorService.UpdateAdmissionConditionForDoctor(doctorId, admissionConditionId, updatedAdmissionCondition);

            return Ok();
        }

        [HttpGet("getAdmissionByClinicAndDoctor/{doctorId}/{clinicId}")]
        public async Task<IActionResult> GetAdmissionByClinicAndDoctor(Guid doctorId, Guid clinicId)
        {
            var x = await _doctorService.GetAdmissionByClinicAndDoctor(doctorId, clinicId);
            return Ok(x);
        }

        [HttpGet("getAdmissionByClinicForDoctor/{clinicId}")]
        public async Task<IActionResult> GetAdmissionByClinicForDoctor(Guid doctorId, Guid clinicId)
        {
            var x = await _doctorService.GetAdmissionByClinicForDoctor(clinicId);
            return Ok(x);
        }

        [HttpGet("clinics")]
        public async Task<ActionResult<string>> GetClinicsForDoctor()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };

            var clinics = await _doctorService.GetClinicsForDoctor();

            var clinicsJson = JsonSerializer.Serialize(clinics, options);

            return Ok(clinicsJson);
        }

        [HttpDelete("clinics/{clinicId}")]
        public async Task<IActionResult> ResignFromClinic(Guid clinicId)
        {
            await _doctorService.ResignFromClinic(clinicId);
            return NoContent();
        }
    }
}
