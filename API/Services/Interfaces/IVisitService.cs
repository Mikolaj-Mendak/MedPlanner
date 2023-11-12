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
        Task<List<Visit>> GetUserIncomingVisits(int page = 1, int pageSize = 10, string firstName = null, string lastName = null, string pesel = null, string sortBy = null);
        Task<List<Visit>> GetUserPreviousVisits(int page = 1, int pageSize = 10, string firstName = null, string lastName = null, string pesel = null, string sortBy = null);
        Task<List<Visit>> GetDoctorIncomingVisits(int page, int pageSize, string firstName = null, string lastName = null, string pesel = null, string sortBy = null);
        Task<List<Visit>> GetDoctorPreviousVisits(int page, int pageSize, string firstName = null, string lastName = null, string pesel = null, string sortBy = null);
        Task AddVisitByUser(CreateVisitDto visitDto);
        Task CancelVisit(Guid visitId);
        Task<List<Visit>> GetPreviousActiveVisits();
        Task<List<GetVisitsAppointmentDto>> GetVisitAppointments(int page, int pageSize, string firstName = null, string lastName = null, string address = null, string clinicName = null, string sortBy = null);
        Task<List<DateTime>> GetAllAvailableDateTimesForPatient(Guid doctorId, Guid clinicId);
    }
}
