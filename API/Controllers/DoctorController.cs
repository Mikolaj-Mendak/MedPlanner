using API.Dtos;
using API.Entities;
using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DoctorController : BaseController
    {

        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            var result = await _doctorService.GetAllDoctors();
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

        [HttpPost("{doctorId}/admissionconditions")]
        public async Task<IActionResult> AddAdmissionCondition(Guid doctorId, [FromBody] DoctorAdmissionConditionsDto admissionConditionDto)
        {
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
                DoctorId = doctorId
            };

            await _doctorService.AddAdmissionConditionToDoctor(doctorId, admissionCondition);
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

    }
}
