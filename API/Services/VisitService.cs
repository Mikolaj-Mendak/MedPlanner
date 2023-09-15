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
        public VisitService(DataContext context)
        {
            _context = context;
        }

        //COMMON VISITS
        public async Task<Visit> GetVisit(Guid visitId)
        {
            return await _context.Visits.FindAsync(visitId);
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
            var newVisit = new Visit
            {
                PatientId = visitDto.PatientId,
                VisitDate = visitDto.VisitDate,
                DoctorId = visitDto.DoctorId,
                IsActive = true,
                Fee = visitDto.Fee
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
        public async Task<List<Visit>> GetDoctorIncomingVisits(Guid doctorId)
        {
            var visits = await _context.Visits
               .Where(visit => visit.VisitDate > DateTime.Now && visit.DoctorId == doctorId && visit.IsActive)
               .ToListAsync();

            return visits;

        }

        public async Task<List<Visit>> GetDoctorPreviousVisits(Guid doctorId)
        {
            var visits = await _context.Visits
               .Where(visit => visit.VisitDate <= DateTime.Now && visit.DoctorId == doctorId && visit.IsActive)
               .ToListAsync();

            return visits;
        }

    }
}
