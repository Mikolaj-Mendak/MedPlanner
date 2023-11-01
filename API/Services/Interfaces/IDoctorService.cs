using API.Dtos;
using API.Entities;

namespace API.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<Doctor> GetDoctor(Guid doctorId);
        Task<List<Doctor>> GetAllDoctors();
        Task UpdateDoctor(Guid doctorId, DoctorUpdateDto updatedDoctorDto);
        Task AddAdmissionConditionToDoctor(Guid doctorId, DoctorAdmissionConditions admissionCondition);
        Task DeleteAdmissionConditionForDoctor(Guid doctorId, Guid admissionConditionId);
        Task UpdateAdmissionConditionForDoctor(Guid doctorId, Guid admissionConditionId, DoctorAdmissionConditions updatedAdmissionCondition);
        Task<List<Doctor>> GetDoctorsByClinicId(Guid clinicId);
        Task<DoctorAdmissionConditions> GetAdmissionByClinicAndDoctor(Guid doctorId,Guid clinicId);
        Task<DoctorAdmissionConditions> GetAdmissionByClinicForDoctor(Guid clinicId);
        Task<List<Clinic>> GetClinicsForDoctor();
        Task ResignFromClinic(Guid clinicId);
    }
}
