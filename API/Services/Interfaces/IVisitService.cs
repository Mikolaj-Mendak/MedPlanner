using API.Dtos;
using API.Entities;

namespace API.Services.Interfaces
{
    public interface IVisitService
    {
        Task<Visit> GetVisit(Guid visitId);
        Task<List<Visit>> GetAllActiveVisits();
        Task<List<Visit>> GetInactiveVisits();
        Task<List<Visit>> GetIncomingActiveVisits();
        Task<List<Visit>> GetUserIncomingVisits(Guid patientId);
        Task<List<Visit>> GetUserPreviousVisits(Guid patientId);
        Task<List<Visit>> GetDoctorIncomingVisits(Guid doctorId);
        Task<List<Visit>> GetDoctorPreviousVisits(Guid doctorId);
        Task AddVisitByUser(CreateVisitDto visitDto);
        Task CancelVisit(Guid visitId);
        Task<List<Visit>> GetPreviousActiveVisits();
    }
}
