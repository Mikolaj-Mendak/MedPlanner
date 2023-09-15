using API.Dtos;
using API.Entities;

namespace API.Services.Interfaces
{
    public interface IClinicOwnerService
    {
        Task<ClinicOwner> GetClinicOwner(Guid onwerId);
        Task<List<ClinicOwner>> GetAllOwners();
        Task UpdateOwner(Guid onwerId, OwnerUpdateDto updatedOwnerDto);
        Task<Clinic> AddClinic(Guid ownerId, AddClinicDto addClinicDto);
        Task DeleteClinicAsync(Guid clinicId);
        Task<Clinic> UpdateClinicAsync(Guid clinicId, AddClinicDto clinicDto);
        Task RemoveDoctorFromClinicAsync(Guid clinicId, Guid doctorId);
        Task AddDoctorToClinicAsync(Guid clinicId, Guid doctorId);

    }
}
