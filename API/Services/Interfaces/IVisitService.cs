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
        Task<List<Visit>> GetDoctorIncomingVisits();
        Task<List<Visit>> GetDoctorPreviousVisits(int page, int pageSize, string firstName = null, string lastName = null, string pesel = null, string sortBy = null);
        Task AddVisitByUser(CreateVisitDto visitDto);
        Task CancelVisit(Guid visitId);
        Task<List<Visit>> GetPreviousActiveVisits();
    }
}
