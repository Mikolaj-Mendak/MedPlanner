using API.Dtos;
using API.Entities;
using API.Services;
using API.Services.Interfaces;
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

        // GET api/visits/{visitId}
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

        // GET api/visits/active
        [HttpGet("active")]
        public async Task<ActionResult<List<Visit>>> GetAllActiveVisits()
        {
            var visits = await _visitService.GetAllActiveVisits();
            return Ok(visits);
        }

        // GET api/visits/inactive
        [HttpGet("inactive")]
        public async Task<ActionResult<List<Visit>>> GetInactiveVisits()
        {
            var visits = await _visitService.GetInactiveVisits();
            return Ok(visits);
        }

        // GET api/visits/incoming
        [HttpGet("incoming")]
        public async Task<ActionResult<List<Visit>>> GetIncomingActiveVisits()
        {
            var visits = await _visitService.GetIncomingActiveVisits();
            return Ok(visits);
        }

        // GET api/visits/previous
        [HttpGet("previous")]
        public async Task<ActionResult<List<Visit>>> GetPreviousActiveVisits()
        {
            var visits = await _visitService.GetPreviousActiveVisits();
            return Ok(visits);
        }

        // POST api/visits
        [HttpPost]
        public async Task<ActionResult> AddVisit([FromBody] CreateVisitDto visitDto)
        {
            await _visitService.AddVisitByUser(visitDto);
            return Ok();
        }

        // PUT api/visits/cancel/{visitId}
        [HttpPut("cancel/{visitId}")]
        public async Task<ActionResult> CancelVisit(Guid visitId)
        {
            await _visitService.CancelVisit(visitId);
            return Ok();
        }

        // GET api/visits/user/{patientId}/incoming
        [HttpGet("patient/incoming")]
        public async Task<ActionResult<List<Visit>>> GetUserIncomingVisits(int page = 1, int pageSize = 10, string firstName = null, string lastName = null, string pesel = null, string sortBy = null)
        {
            var visits = await _visitService.GetUserIncomingVisits(page, pageSize, firstName, lastName, pesel, sortBy);
            return Ok(visits);
        }

        // GET api/visits/user/{patientId}/previous
        [HttpGet("patient/history")]
        public async Task<ActionResult<List<Visit>>> GetUserPreviousVisits(int page = 1, int pageSize = 10, string firstName = null, string lastName = null, string pesel = null, string sortBy = null)
        {
            var visits = await _visitService.GetUserPreviousVisits(page, pageSize, firstName, lastName, pesel, sortBy);
            return Ok(visits);
        }


        [HttpGet("doctor/incoming")]
        public async Task<ActionResult<List<Visit>>> GetDoctorIncomingVisits(int page = 1, int pageSize = 10, string firstName = null, string lastName = null, string pesel = null, string sortBy = null)
        {
            var visits = await _visitService.GetDoctorIncomingVisits(page, pageSize, firstName, lastName, pesel, sortBy);
            return Ok(visits);
        }

        [HttpGet("doctor/history")]
        public async Task<ActionResult<List<Visit>>> GetDoctorPreviousVisits(int page = 1, int pageSize = 10, string firstName = null, string lastName = null, string pesel = null, string sortBy = null)
        {
            var visits = await _visitService.GetDoctorPreviousVisits(page, pageSize, firstName, lastName, pesel, sortBy);

            return Ok(visits);
        }

        [HttpGet("visitAppointments")]
        public async Task<ActionResult<List<GetVisitsAppointmentDto>>> GetVisitAppointments(int page = 1, int pageSize = 10, string firstName = null, string lastName = null, string address = null, string clinicName = null, string sortBy = null)
        {
            var visits = await _visitService.GetVisitAppointments(page, pageSize, firstName, lastName, address, clinicName, sortBy);
            return Ok(visits);
        }

        [HttpGet("avaliableVisitDates/{doctorId}/{clinicId}")]
        public async Task<ActionResult<List<DateTime>>> GetAvaliableDates(Guid doctorId, Guid clinicId)
        {
            var avaliableDates = await _visitService.GetAllAvailableDateTimesForPatient(doctorId, clinicId);
            return Ok(avaliableDates);
        }

    }
}
