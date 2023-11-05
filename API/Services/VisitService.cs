using API.Data;
using API.Dtos;
using API.Entities;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace API.Services
{
    public class VisitService : IVisitService
    {
        private readonly DataContext _context;
        private readonly IUserProviderService _userProviderService;
        private readonly IUsersService _userService;
        private readonly IDoctorService _doctorService;
        public VisitService(DataContext context, IUserProviderService userProviderService, IUsersService userService, IDoctorService doctorService)
        {
            _context = context;
            _userProviderService = userProviderService;
            _doctorService = doctorService;
            _userService = userService;
            _doctorService = doctorService;
        }

        //COMMON VISITS
        public async Task<Visit> GetVisit(Guid visitId)
        {
            var doctorId = _userProviderService.GetCurrentUserId();
            var visit = await _context.Visits.SingleOrDefaultAsync(x => x.Id == visitId);

            if (visit != null)
            {
                visit.Clinic = _context.Clinics.FirstOrDefault(x => x.Id == visit.ClinicId);
                visit.Patient = await _userService.GetUserByIdAsync(visit.PatientId);
                visit.Doctor = await _doctorService.GetDoctor(visit.DoctorId);
            }

            return visit;
        }


        public async Task<List<Visit>> GetAllActiveVisits()
        {
            return await _context.Visits.Where(x => x.IsActive).ToListAsync();
        }

        public async Task<List<Visit>> GetInactiveVisits()
        {
            return await _context.Visits.Where(x => !x.IsActive).ToListAsync();
        }

        public async Task<List<Visit>> GetIncomingActiveVisits()
        {
            var visits = await _context.Visits
               .Where(visit => visit.VisitDate > DateTime.Now && visit.IsActive)
               .ToListAsync();

            return visits;
        }

        public async Task<List<Visit>> GetPreviousActiveVisits()
        {
            var visits = await _context.Visits
               .Where(visit => visit.VisitDate <= DateTime.Now && visit.IsActive)
               .ToListAsync();

            return visits;
        }

        public async Task CancelVisit(Guid visitId)
        {
            var visit = await GetVisit(visitId);
            visit.IsActive = false;
            await _context.SaveChangesAsync();
        }

        //PATIENT VISITS
        public async Task<List<Visit>> GetUserIncomingVisits(Guid patientId)
        {
            var visits = await _context.Visits
               .Where(visit => visit.VisitDate > DateTime.Now && visit.PatientId == patientId && visit.IsActive)
               .ToListAsync();

            return visits;
        }

        public async Task AddVisitByUser(CreateVisitDto visitDto)
        {
            var patient = await _userService.GetUserByIdAsync(visitDto.PatientId);
            var doctor = await _doctorService.GetDoctor(visitDto.DoctorId);
            var doctorAdmission = await _doctorService.GetAdmissionByClinicAndDoctor(visitDto.DoctorId, visitDto.ClinicId);

            var newVisit = new Visit
            {
                PatientId = visitDto.PatientId,
                VisitDate = visitDto.VisitDate,
                DoctorId = visitDto.DoctorId,
                IsActive = true,
                Fee = doctorAdmission.ConsultationFee,
                Patient = patient,
                Doctor = doctor,
                ClinicId = visitDto.ClinicId,
            };

            _context.Visits.Add(newVisit);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Visit>> GetUserPreviousVisits(Guid patientId)
        {
            var visits = await _context.Visits
               .Where(visit => visit.VisitDate <= DateTime.Now && visit.PatientId == patientId && visit.IsActive)
               .ToListAsync();

            return visits;
        }

        //DOCTOR VISITS
        public async Task<List<Visit>> GetDoctorIncomingVisits()
        {
            var doctorId = _userProviderService.GetCurrentUserId();
            var visits = await _context.Visits
                .Where(visit => visit.VisitDate > DateTime.Now && visit.DoctorId.ToString() == doctorId )
                .ToListAsync();

            foreach (var visit in visits)
            {
                visit.Clinic =  _context.Clinics.FirstOrDefault(x => x.Id == visit.ClinicId);
                visit.Patient = await _userService.GetUserByIdAsync(visit.PatientId);
                visit.Doctor = await _doctorService.GetDoctor(visit.DoctorId);
            }

            return visits;
        }

        public async Task<List<Visit>> GetDoctorPreviousVisits()
        {
            var doctorId = _userProviderService.GetCurrentUserId();
            var visits = await _context.Visits
                .Where(visit => visit.VisitDate <= DateTime.Now && visit.DoctorId.ToString() == doctorId )
                .ToListAsync();

            foreach (var visit in visits)
            {
                visit.Clinic =  _context.Clinics.FirstOrDefault(x => x.Id == visit.ClinicId);
                visit.Patient = await _userService.GetUserByIdAsync(visit.PatientId);
                visit.Doctor = await _doctorService.GetDoctor(visit.DoctorId);
            }

            return visits;
        }


    }
}
