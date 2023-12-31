﻿using API.Dtos;
using API.Entities;
using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Globalization;
using API.Authorization;
using API.Enums;

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

        [RolesAuthorization(UserRoleEnum.ClinicOwner, UserRoleEnum.User, UserRoleEnum.Doctor)]
        [HttpGet("doctorsByClinic/{clinicId}")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorsByClinicId(
            Guid clinicId,
            int page = 1,
            int pageSize = 10,
            string firstName = null,
            string lastName = null,
            string pesel = null,
            string doctorNumber = null,
            string phoneNumber = null
        )
        {
            var result = await _doctorService.GetDoctorsByClinicId(
                clinicId,
                page,
                pageSize,
                firstName,
                lastName,
                pesel,
                doctorNumber
            );

            return Ok(result);
        }

        [RolesAuthorization(UserRoleEnum.ClinicOwner, UserRoleEnum.User, UserRoleEnum.Doctor)]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctor(Guid id)
        {
            var result = await _doctorService.GetDoctor(id);
            return Ok(result);
        }

        [RolesAuthorization(UserRoleEnum.ClinicOwner, UserRoleEnum.User, UserRoleEnum.Doctor)]
        [HttpPut("{doctorId}")]
        public async Task<IActionResult> UpdateDoctor(Guid doctorId, [FromBody] DoctorUpdateDto doctorUpdateDto)
        {
            await _doctorService.UpdateDoctor(doctorId, doctorUpdateDto);

            return NoContent();
        }

        [RolesAuthorization( UserRoleEnum.Doctor)]
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

        [RolesAuthorization(UserRoleEnum.Doctor)]
        [HttpDelete("{doctorId}/admissionconditions/{admissionConditionId}")]
        public async Task<IActionResult> DeleteAdmissionCondition(Guid doctorId, Guid admissionConditionId)
        {
            await _doctorService.DeleteAdmissionConditionForDoctor(doctorId, admissionConditionId);
            return NoContent();
        }

        [RolesAuthorization(UserRoleEnum.Doctor)]
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

        [RolesAuthorization(UserRoleEnum.ClinicOwner, UserRoleEnum.User, UserRoleEnum.Doctor)]
        [HttpGet("getAdmissionByClinicAndDoctor/{doctorId}/{clinicId}")]
        public async Task<IActionResult> GetAdmissionByClinicAndDoctor(Guid doctorId, Guid clinicId)
        {
            var x = await _doctorService.GetAdmissionByClinicAndDoctor(doctorId, clinicId);
            return Ok(x);
        }

        [RolesAuthorization(UserRoleEnum.ClinicOwner, UserRoleEnum.User, UserRoleEnum.Doctor)]
        [HttpGet("getAdmissionByClinicForDoctor/{clinicId}")]
        public async Task<IActionResult> GetAdmissionByClinicForDoctor(Guid doctorId, Guid clinicId)
        {
            var x = await _doctorService.GetAdmissionByClinicForDoctor(clinicId);
            return Ok(x);
        }

        [RolesAuthorization(UserRoleEnum.ClinicOwner, UserRoleEnum.User, UserRoleEnum.Doctor)]
        [HttpGet("clinics")]
        public async Task<ActionResult<string>> GetClinicsForDoctor(int page = 1, int pageSize = 10, string name = null, string address = null)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };

            var clinics = await _doctorService.GetClinicsForDoctor(page, pageSize, name, address);

            var clinicsJson = JsonSerializer.Serialize(clinics, options);

            return Ok(clinicsJson);
        }

        [RolesAuthorization(UserRoleEnum.Doctor)]
        [HttpDelete("clinics/{clinicId}")]
        public async Task<IActionResult> ResignFromClinic(Guid clinicId)
        {
            await _doctorService.ResignFromClinic(clinicId);
            return NoContent();
        }
    }
}
