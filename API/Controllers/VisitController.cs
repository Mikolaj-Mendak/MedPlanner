using API.Authorization;
using API.Dtos;
using API.Entities;
using API.Enums;
using API.Services;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace API.Controllers
{
    public class VisitController : BaseController
    {
        private readonly IVisitService _visitService;

        public VisitController(IVisitService visitService)
        {
            _visitService = visitService;
        }

        [RolesAuthorization(UserRoleEnum.Doctor, UserRoleEnum.User)]
        [HttpGet("{visitId}")]
        public async Task<ActionResult<Visit>> GetVisit(Guid visitId)
        {
            var visit = await _visitService.GetVisit(visitId);
            if (visit == null)
            {
                return NotFound();
            }

            return Ok(visit);
        }

        [RolesAuthorization(UserRoleEnum.User)]
        [HttpPost]
        public async Task<ActionResult> AddVisit([FromBody] CreateVisitDto visitDto)
        {
            await _visitService.AddVisitByUser(visitDto);
            return Ok();
        }

        [RolesAuthorization(UserRoleEnum.Doctor, UserRoleEnum.User)]
        [HttpPut("cancel/{visitId}")]
        public async Task<ActionResult> CancelVisit(Guid visitId)
        {
            await _visitService.CancelVisit(visitId);
            return Ok();
        }

        [RolesAuthorization(UserRoleEnum.User)]
        [HttpGet("patient/incoming")]
        public async Task<ActionResult<List<Visit>>> GetUserIncomingVisits(int page = 1, int pageSize = 10, string firstName = null, string lastName = null, string pesel = null, string sortBy = null)
        {
            var visits = await _visitService.GetUserIncomingVisits(page, pageSize, firstName, lastName, pesel, sortBy);
            return Ok(visits);
        }

        [RolesAuthorization(UserRoleEnum.Doctor, UserRoleEnum.User)]
        [HttpGet("patient/history")]
        public async Task<ActionResult<List<Visit>>> GetUserPreviousVisits(int page = 1, int pageSize = 10, string firstName = null, string lastName = null, string pesel = null, string sortBy = null)
        {
            var visits = await _visitService.GetUserPreviousVisits(page, pageSize, firstName, lastName, pesel, sortBy);
            return Ok(visits);
        }

        [RolesAuthorization(UserRoleEnum.Doctor)]
        [HttpGet("doctor/incoming")]
        public async Task<ActionResult<List<Visit>>> GetDoctorIncomingVisits(int page = 1, int pageSize = 10, string firstName = null, string lastName = null, string pesel = null, string sortBy = null)
        {
            var visits = await _visitService.GetDoctorIncomingVisits(page, pageSize, firstName, lastName, pesel, sortBy);
            return Ok(visits);
        }

        [RolesAuthorization(UserRoleEnum.Doctor)]
        [HttpGet("doctor/history")]
        public async Task<ActionResult<List<Visit>>> GetDoctorPreviousVisits(int page = 1, int pageSize = 10, string firstName = null, string lastName = null, string pesel = null, string sortBy = null)
        {
            var visits = await _visitService.GetDoctorPreviousVisits(page, pageSize, firstName, lastName, pesel, sortBy);

            return Ok(visits);
        }

        [RolesAuthorization(UserRoleEnum.User)]
        [HttpGet("visitAppointments")]
        public async Task<ActionResult<List<GetVisitsAppointmentDto>>> GetVisitAppointments(int page = 1, int pageSize = 10, string firstName = null, string lastName = null, string address = null, string clinicName = null, string specialization = null, string sortBy = null)
        {
            var visits = await _visitService.GetVisitAppointments(page, pageSize, firstName, lastName, address, clinicName, specialization, sortBy);
            return Ok(visits);
        }

        [RolesAuthorization(UserRoleEnum.User)]
        [HttpGet("avaliableVisitDates/{doctorId}/{clinicId}")]
        public async Task<ActionResult<List<DateTime>>> GetAvaliableDates(Guid doctorId, Guid clinicId)
        {
            var avaliableDates = await _visitService.GetAllAvailableDateTimesForPatient(doctorId, clinicId);
            return Ok(avaliableDates);
        }

    }
}
