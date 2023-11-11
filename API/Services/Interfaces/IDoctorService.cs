using API.Dtos;
using API.Entities;

namespace API.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<Doctor> GetDoctor(Guid doctorId);
        Task<List<Doctor>> GetAllDoctors();
        Task UpdateDoctor(Guid doctorId, DoctorUpdateDto updatedDoctorDto);
        Task AddAdmissionConditionToDoctor(DoctorAdmissionConditions admissionCondition);
        Task DeleteAdmissionConditionForDoctor(Guid doctorId, Guid admissionConditionId);
        Task UpdateAdmissionConditionForDoctor(Guid doctorId, Guid admissionConditionId, DoctorAdmissionConditions updatedAdmissionCondition);
        Task<List<Doctor>> GetDoctorsByClinicId(
          Guid clinicId,
          int page = 1,
          int pageSize = 10,
          string firstName = null,
          string lastName = null,
          string pesel = null,
          string doctorNumber = null
        );
        Task<DoctorAdmissionConditions> GetAdmissionByClinicAndDoctor(Guid doctorId,Guid clinicId);
        Task<DoctorAdmissionConditions> GetAdmissionByClinicForDoctor(Guid clinicId);
        Task<List<Clinic>> GetClinicsForDoctor(int page = 1, int pageSize = 10, string name = null, string address = null);
        Task ResignFromClinic(Guid clinicId);
    }
}
