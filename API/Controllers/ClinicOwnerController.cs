using API.Dtos;
using API.Entities;
using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ClinicOwnerController : BaseController
    {

            private readonly IClinicOwnerService _clinicOwnerService;

            public ClinicOwnerController(IClinicOwnerService clinicOwnerService)
            {
                _clinicOwnerService = clinicOwnerService;
            }


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

            [HttpGet]
            public async Task<ActionResult<List<ClinicOwner>>> GetAllOwners()
            {
                var owners = await _clinicOwnerService.GetAllOwners();
                return Ok(owners);
            }

            [HttpPut("{ownerId}")]
            public async Task<IActionResult> UpdateOwner(Guid ownerId, [FromBody] OwnerUpdateDto updatedOwnerDto)
            {
                    await _clinicOwnerService.UpdateOwner(ownerId, updatedOwnerDto);
                    return NoContent();
            }

            [HttpPost("clinics")]
            public async Task<ActionResult<Clinic>> AddClinic([FromBody] AddClinicDto addClinicDto)
            {
                var clinic = await _clinicOwnerService.AddClinic(addClinicDto);
                return Ok(clinic);
            }

        [HttpDelete("{clinicId}/clinics")]
            public async Task<IActionResult> DeleteClinic(Guid clinicId)
            {
                await _clinicOwnerService.DeleteClinicAsync(clinicId);
                return NoContent();
            }


            [HttpGet("clinics")]
            public async Task<ActionResult<List<Clinic>>> GetAllClinics()
            {
                var owners = await _clinicOwnerService.GetAllClinics();
                return Ok(owners);
            }

            [HttpGet("clinics/{clinicId}")]
            public async Task<ActionResult<Clinic>> GetClinicById(Guid clinicId)
            {
                var owner = await _clinicOwnerService.GetClinicById(clinicId);
                return Ok(owner);
            }


        [HttpPost("clinics/{clinicId}/doctors/{doctorId}")]
            public async Task<IActionResult> AddDoctorToClinic(Guid clinicId, Guid doctorId)
            {
                    await _clinicOwnerService.AddDoctorToClinicAsync(clinicId, doctorId);
                    return NoContent();
            }

            [HttpDelete("clinics/{clinicId}/doctors/{doctorId}")]
            public async Task<IActionResult> RemoveDoctorFromClinic(Guid clinicId, Guid doctorId)
            {
                await _clinicOwnerService.RemoveDoctorFromClinicAsync(clinicId, doctorId);
                return NoContent();
            }
        }

    
    }
