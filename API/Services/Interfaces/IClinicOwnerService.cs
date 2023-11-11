using API.Dtos;
using API.Entities;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IClinicOwnerService
    {
        Task<ClinicOwner> GetClinicOwner(Guid onwerId);
        Task<List<ClinicOwner>> GetAllOwners();
        Task UpdateOwner(Guid onwerId, OwnerUpdateDto updatedOwnerDto);
        Task<Clinic> AddClinic(AddClinicDto addClinicDto);
        Task DeleteClinicAsync(Guid clinicId);
        Task<Clinic> UpdateClinicAsync(Guid clinicId, AddClinicDto clinicDto);
        Task RemoveDoctorFromClinicAsync(Guid clinicId, Guid doctorId);
        Task AddDoctorToClinicAsync(Guid clinicId, Guid doctorId);
        Task<List<Clinic>> GetAllClinics(int page = 1, int pageSize = 10, string name = null, string address = null);
        Task<Clinic> GetClinicById(Guid clinicId);
        Task AddDoctorToClinicByNumber(Guid clinicId, string doctorNumber);

    }
}
