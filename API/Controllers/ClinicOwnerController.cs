using API.Authorization;
using API.Dtos;
using API.Entities;
using API.Enums;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ClinicOwnerController : BaseController
    {

            private readonly IClinicOwnerService _clinicOwnerService;

            public ClinicOwnerController(IClinicOwnerService clinicOwnerService)
            {
                _clinicOwnerService = clinicOwnerService;
            }

            [RolesAuthorization(UserRoleEnum.ClinicOwner)]
            [HttpGet("{ownerId}")]
        public async Task<ActionResult<ClinicOwner>> GetClinicOwner(Guid ownerId)
            {
                var owner = await _clinicOwnerService.GetClinicOwner(ownerId);

                if (owner == null)
                {
                    return NotFound();
                }

                return Ok(owner);
            }


            [RolesAuthorization(UserRoleEnum.ClinicOwner)]
            [HttpPut("{ownerId}")]
            public async Task<IActionResult> UpdateOwner(Guid ownerId, [FromBody] OwnerUpdateDto updatedOwnerDto)
            {
                    await _clinicOwnerService.UpdateOwner(ownerId, updatedOwnerDto);
                    return NoContent();
            }

            [RolesAuthorization(UserRoleEnum.ClinicOwner)]
            [HttpPost("clinics")]
            public async Task<ActionResult<Clinic>> AddClinic([FromBody] AddClinicDto addClinicDto)
            {
                var clinic = await _clinicOwnerService.AddClinic(addClinicDto);
                return Ok(clinic);
            }

            [RolesAuthorization(UserRoleEnum.ClinicOwner)]
            [HttpDelete("{clinicId}/clinics")]
            public async Task<IActionResult> DeleteClinic(Guid clinicId)
            {
                await _clinicOwnerService.DeleteClinicAsync(clinicId);
                return NoContent();
            }

            [RolesAuthorization(UserRoleEnum.ClinicOwner, UserRoleEnum.User,UserRoleEnum.Doctor)]
            [HttpGet("clinics")]
            public async Task<ActionResult<List<Clinic>>> GetAllClinics(int page = 1, int pageSize = 10, string name = null, string address = null)
            {
                var owners = await _clinicOwnerService.GetAllClinics(page, pageSize, name, address);
                return Ok(owners);
            }

            [RolesAuthorization(UserRoleEnum.ClinicOwner, UserRoleEnum.User, UserRoleEnum.Doctor)]
            [HttpGet("clinics/{clinicId}")]
            public async Task<ActionResult<Clinic>> GetClinicById(Guid clinicId)
            {
                var owner = await _clinicOwnerService.GetClinicById(clinicId);
                return Ok(owner);
            }

            [RolesAuthorization(UserRoleEnum.ClinicOwner)]
            [HttpPost("clinics/{clinicId}/doctors/{doctorId}")]
            public async Task<IActionResult> AddDoctorToClinic(Guid clinicId, Guid doctorId)
            {
                    await _clinicOwnerService.AddDoctorToClinicAsync(clinicId, doctorId);
                    return NoContent();
            }


            [RolesAuthorization(UserRoleEnum.ClinicOwner)]
            [HttpDelete("removeDoctor/clinics/{clinicId}/doctors/{doctorId}")]
            public async Task<IActionResult> RemoveDoctorFromClinic(Guid clinicId, Guid doctorId)
            {
                await _clinicOwnerService.RemoveDoctorFromClinicAsync(clinicId, doctorId);
                return NoContent();
            }


            [RolesAuthorization(UserRoleEnum.ClinicOwner)]
            [HttpPost("clinics/{clinicId}/doctorNumber/{doctorNumber}")]
            public async Task<IActionResult> AddDoctorToClinicByNumber(Guid clinicId, string doctorNumber)
            {
                await _clinicOwnerService.AddDoctorToClinicByNumber(clinicId, doctorNumber);
                return NoContent();
            }

    }
}
