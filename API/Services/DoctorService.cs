using API.Data;
using API.Dtos;
using API.Entities;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly DataContext _context;

        public DoctorService(DataContext context)
        {
            _context=context;
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

        public async Task AddAdmissionConditionToDoctor(Guid doctorId, DoctorAdmissionConditions admissionCondition)
        {
            var doctor = await _context.Doctors.Include(d => d.AdmissionConditions).FirstOrDefaultAsync(d => d.Id == doctorId);

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


    }
}
