using API.Data;
using API.Dtos;
using API.Entities;
using API.Migrations;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly DataContext _context;
        private readonly IUserProviderService _currentUserService;

        public DoctorService(DataContext context, IUserProviderService currentUserService)
        {
            _context=context;
            _currentUserService=currentUserService;
        }

        public async Task<Doctor> GetDoctor(Guid doctorId)
        {
            return await _context.Doctors.FindAsync(doctorId);
        }

        public async Task<List<Doctor>> GetAllDoctors()
        {
            return await _context.Doctors.Where(u => u is Doctor).ToListAsync();
        }

        public async Task UpdateDoctor(Guid doctorId, DoctorUpdateDto updatedDoctorDto)
        {
            var existingDoctor = await _context.Doctors.FindAsync(doctorId);

            if (existingDoctor == null)
            {
                throw new Exception("Lekarz nie został odnaleziony.");
            }

            if (updatedDoctorDto.FirstName != null)
            {
                existingDoctor.FirstName = updatedDoctorDto.FirstName;
            }

            if (updatedDoctorDto.LastName != null)
            {
                existingDoctor.LastName = updatedDoctorDto.LastName;
            }

            if (updatedDoctorDto.Email != null)
            {
                existingDoctor.Email = updatedDoctorDto.Email;
            }

            if (updatedDoctorDto.DoctorNumber != null)
            {
                existingDoctor.DoctorNumber = updatedDoctorDto.DoctorNumber;
            }

            if (updatedDoctorDto.Pesel != null)
            {
                existingDoctor.Pesel = updatedDoctorDto.Pesel;
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddAdmissionConditionToDoctor(DoctorAdmissionConditions admissionCondition)
        {
            var doctor = await _context.Doctors.Include(d => d.AdmissionConditions).FirstOrDefaultAsync(d => d.Id == admissionCondition.DoctorId);

            if (doctor == null)
            {
                throw new Exception("Lekarz nie został odnaleziony.");
            }

            admissionCondition.Doctor = doctor;

            doctor.AdmissionConditions.Add(admissionCondition);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAdmissionConditionForDoctor(Guid doctorId, Guid admissionConditionId)
        {
            var doctor = await _context.Doctors.Include(d => d.AdmissionConditions).FirstOrDefaultAsync(d => d.Id == doctorId);

            if (doctor == null)
            {
                throw new Exception("Lekarz nie został odnaleziony.");
            }

            var admissionCondition = doctor.AdmissionConditions.FirstOrDefault(ac => ac.Id == admissionConditionId);

            if (admissionCondition == null)
            {
                throw new Exception("Warunek przyjęcia nie został odnaleziony.");
            }

           _context.Remove(admissionCondition);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAdmissionConditionForDoctor(Guid doctorId, Guid admissionConditionId, DoctorAdmissionConditions updatedAdmissionCondition)
        {
            var doctor = await _context.Doctors.Include(d => d.AdmissionConditions).FirstOrDefaultAsync(d => d.Id == doctorId);

            if (doctor == null)
            {
                throw new Exception("Lekarz nie został odnaleziony.");
            }

            var admissionCondition = doctor.AdmissionConditions.FirstOrDefault(ac => ac.Id == admissionConditionId);

            if (admissionCondition == null)
            {
                throw new Exception("Warunek przyjęcia nie został odnaleziony.");
            }

            admissionCondition.Specialization = updatedAdmissionCondition.Specialization;
            admissionCondition.IsNFZ = updatedAdmissionCondition.IsNFZ;
            admissionCondition.IsPrivate = updatedAdmissionCondition.IsPrivate;
            admissionCondition.ConsultationFee = updatedAdmissionCondition.ConsultationFee;
            admissionCondition.WorkingDays = updatedAdmissionCondition.WorkingDays;
            admissionCondition.WorkHoursStart = updatedAdmissionCondition.WorkHoursStart;
            admissionCondition.WorkHoursEnd = updatedAdmissionCondition.WorkHoursEnd;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Doctor>> GetDoctorsByClinicId(
            Guid clinicId,
            int page = 1,
            int pageSize = 10,
            string firstName = null,
            string lastName = null,
            string pesel = null,
            string doctorNumber = null
        )
        {
            var query = _context.ClinicDoctors
                .Include(cd => cd.Doctor)
                .Where(cd => cd.ClinicId == clinicId);

            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(cd => cd.Doctor.FirstName.Contains(firstName));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(cd => cd.Doctor.LastName.Contains(lastName));
            }

            if (!string.IsNullOrEmpty(pesel))
            {
                query = query.Where(cd => cd.Doctor.Pesel.Contains(pesel));
            }

            if (!string.IsNullOrEmpty(doctorNumber))
            {
                query = query.Where(cd => cd.Doctor.DoctorNumber.Contains(doctorNumber));
            }


            var doctors = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(cd => cd.Doctor)
                .ToListAsync();

            return doctors;
        }


        public async Task<DoctorAdmissionConditions> GetAdmissionByClinicAndDoctor(Guid doctorId, Guid clinicId)
        {
            var x = await _context.DoctorAdmissionConditions.FirstOrDefaultAsync(x => x.DoctorId == doctorId && x.ClinicId == clinicId);
            return x;
        }

        public async Task<DoctorAdmissionConditions> GetAdmissionByClinicForDoctor(Guid clinicId)
        {
            var doctorId = _currentUserService.GetCurrentUserId();
            var x = await _context.DoctorAdmissionConditions.FirstOrDefaultAsync(x => x.DoctorId.ToString() == doctorId && x.ClinicId == clinicId);
            return x;
        }

        public async Task<List<Clinic>> GetClinicsForDoctor(int page = 1, int pageSize = 10, string name = null, string address = null)
        {
            var doctorId = _currentUserService.GetCurrentUserId();

            var clinicDoctorRelationsQuery = _context.ClinicDoctors
                .Where(x => x.DoctorId.ToString() == doctorId);

            if (!string.IsNullOrEmpty(name))
            {
                clinicDoctorRelationsQuery = clinicDoctorRelationsQuery
                    .Where(x => x.Clinic.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(address))
            {
                clinicDoctorRelationsQuery = clinicDoctorRelationsQuery
                    .Where(x => x.Clinic.Address.Contains(address));
            }

            var clinicIds = await clinicDoctorRelationsQuery
                .Select(x => x.ClinicId)
                .ToListAsync();

            var clinics = await _context.Clinics
                .Where(x => clinicIds.Contains(x.Id.Value))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return clinics;
        }


        public async Task ResignFromClinic(Guid clinicId)
        {
            var clinicDoctorRelation = await _context.ClinicDoctors.FirstOrDefaultAsync(x => x.ClinicId == clinicId);
            _context.Remove(clinicDoctorRelation);
            await _context.SaveChangesAsync();
        }

    }
}
