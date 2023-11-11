using API.Data;
using API.Dtos;
using API.Entities;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ClinicOwnerService : IClinicOwnerService
    {
        private readonly DataContext _context;
        private readonly IUserProviderService _currentUserService;

        public ClinicOwnerService(DataContext context, IUserProviderService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<ClinicOwner> GetClinicOwner(Guid onwerId)
        {
            var clinicOwnerWithClinics = await _context.ClinicOwners
             .Include(co => co.Clinic)
             .FirstOrDefaultAsync(co => co.Id == onwerId);

            return clinicOwnerWithClinics;
        }

        public async Task<List<ClinicOwner>> GetAllOwners()
        {
            return await _context.ClinicOwners.Where(u => u is ClinicOwner).ToListAsync();
        }

        public async Task UpdateOwner(Guid ownerId, OwnerUpdateDto updatedOwnerDto)
        {
            var existingOwner = await _context.ClinicOwners.FindAsync(ownerId);

            if (existingOwner == null)
            {
                throw new Exception("Nie został odnaleziony.");
            }

            if (updatedOwnerDto.FirstName != null)
            {
                existingOwner.FirstName = updatedOwnerDto.FirstName;
            }

            if (updatedOwnerDto.LastName != null)
            {
                existingOwner.LastName = updatedOwnerDto.LastName;
            }

            if (updatedOwnerDto.Email != null)
            {
                existingOwner.Email = updatedOwnerDto.Email;
            }

            if (updatedOwnerDto.Pesel != null)
            {
                existingOwner.Pesel = updatedOwnerDto.Pesel;
            }

            if (updatedOwnerDto.Clinic != null)
            {
                existingOwner.Clinic = updatedOwnerDto.Clinic;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Clinic> AddClinic(AddClinicDto addClinicDto)
        {
            var clinicOwner = _currentUserService.GetCurrentUserId();

            if (clinicOwner == null)
            {
                throw new InvalidOperationException("ClinicOwner not found.");
            }

            if (await _context.Clinics.AnyAsync(c => c.Name == addClinicDto.Name || c.Address == addClinicDto.Address))
            {
                throw new InvalidOperationException("A clinic with the same name or address already exists.");
            }

            var newClinic = new Clinic
            {
                Name = addClinicDto.Name,
                Address = addClinicDto.Address,
                NIP = addClinicDto.NIP,
                IsNFZ = addClinicDto.IsNFZ,
                IsPrivate = addClinicDto.IsPrivate,
                ClinicOwnerId = Guid.Parse(clinicOwner),
                OfficeHoursStartDate = addClinicDto.OfficeHoursStartDate,
                OfficeHoursEndDate = addClinicDto.OfficeHoursEndDate,
                PhoneNumber = addClinicDto.PhoneNumber
            };

            _context.Clinics.Add(newClinic);
            await _context.SaveChangesAsync();

            return newClinic;

        }

        public async Task<Clinic> UpdateClinicAsync(Guid clinicId, AddClinicDto clinicDto)
        {
            var clinic = await _context.Clinics.FindAsync(clinicId);

            if (clinic == null)
            {
                throw new InvalidOperationException("Clinic not found.");
            }

            if (await _context.Clinics.AnyAsync(c => c.Id != clinicId && (c.Name == clinicDto.Name || c.Address == clinicDto.Address)))
            {
                throw new InvalidOperationException("Another clinic with the same name or address already exists.");
            }

            clinic.Name = clinicDto.Name;
            clinic.Address = clinicDto.Address;
            clinic.NIP = clinicDto.NIP;
            clinic.IsNFZ = clinicDto.IsNFZ;
            clinic.IsPrivate = clinicDto.IsPrivate;
            clinic.OfficeHoursStartDate = clinicDto.OfficeHoursStartDate;
            clinic.OfficeHoursEndDate = clinicDto.OfficeHoursEndDate;

            await _context.SaveChangesAsync();

            return clinic;
        }

        public async Task DeleteClinicAsync(Guid clinicId)
        {
            var clinic = await _context.Clinics.FindAsync(clinicId);

            if (clinic == null)
            {
                throw new InvalidOperationException("Clinic not found.");
            }

            _context.Clinics.Remove(clinic);
            await _context.SaveChangesAsync();
        }

         public async Task AddDoctorToClinicAsync(Guid clinicId, Guid doctorId)
        {
            var clinic = await _context.Clinics.FindAsync(clinicId);
            if (clinic == null)
            {
                throw new ApplicationException("Klinika nie istnieje.");
            }

            var doctor = await _context.Doctors.FindAsync(doctorId);
            if (doctor == null)
            {
                throw new ApplicationException("Lekarz nie istnieje.");
            }

            var existingRelationship = await _context.ClinicDoctors.FindAsync(clinicId, doctorId);
            if (existingRelationship != null)
            {
                throw new ApplicationException("Lekarz jest już przypisany do kliniki.");
            }

            var clinicDoctor = new ClinicDoctor
            {
                ClinicId = clinicId,
                DoctorId = doctorId
            };

            _context.ClinicDoctors.Add(clinicDoctor);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveDoctorFromClinicAsync(Guid clinicId, Guid doctorId)
        {
            var clinic = await _context.Clinics.FindAsync(clinicId);
            if (clinic == null)
            {
                throw new ApplicationException("Klinika nie istnieje.");
            }

            var doctor = await _context.Doctors.FindAsync(doctorId);
            if (doctor == null)
            {
                throw new ApplicationException("Lekarz nie istnieje.");
            }

            var clinicDoctor = await _context.ClinicDoctors.FindAsync(clinicId, doctorId);
            if (clinicDoctor == null)
            {
                throw new ApplicationException("Lekarz nie jest przypisany do tej kliniki.");
            }

            _context.ClinicDoctors.Remove(clinicDoctor);
            await _context.SaveChangesAsync();
        }


        public async Task<List<Clinic>> GetAllClinics(int page = 1, int pageSize = 10, string name = null, string address = null)
        {
            var user = _currentUserService.GetCurrentUserId();

            var clinicsQuery = _context.Clinics
                .Where(x => x.ClinicOwnerId.ToString() == user);

            if (!string.IsNullOrEmpty(name))
            {
                clinicsQuery = clinicsQuery.Where(x => x.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(address))
            {
                clinicsQuery = clinicsQuery.Where(x => x.Address.Contains(address));
            }

            var clinics = await clinicsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return clinics;
        }

        public async Task<Clinic> GetClinicById(Guid clinicId)
        {
            var clinic = await _context.Clinics.FirstOrDefaultAsync(x => x.Id == clinicId);
            return clinic;
        }

        public async Task AddDoctorToClinicByNumber(Guid clinicId, string doctorNumber)
        {
            var clinic = await _context.Clinics.FindAsync(clinicId);
            if (clinic == null)
            {
                throw new ApplicationException("Klinika nie istnieje.");
            }

            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.DoctorNumber == doctorNumber);
            if (doctor == null)
            {
                throw new ApplicationException("Lekarz nie istnieje.");
            }

            var existingRelationship = await _context.ClinicDoctors.FindAsync(clinicId, doctor.Id);
            if (existingRelationship != null)
            {
                throw new ApplicationException("Lekarz jest już przypisany do kliniki.");
            }

            var clinicDoctor = new ClinicDoctor
            {
                ClinicId = clinicId,
                DoctorId = doctor.Id
            };

            _context.ClinicDoctors.Add(clinicDoctor);
            await _context.SaveChangesAsync();
        }

    }
}
