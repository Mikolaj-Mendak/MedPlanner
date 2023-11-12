using API.Data;
using API.Dtos;
using API.Entities;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace API.Services
{
    public class VisitService : IVisitService
    {
        private readonly DataContext _context;
        private readonly IUserProviderService _userProviderService;
        private readonly IUsersService _userService;
        private readonly IDoctorService _doctorService;
        public VisitService(DataContext context, IUserProviderService userProviderService, IUsersService userService, IDoctorService doctorService)
        {
            _context = context;
            _userProviderService = userProviderService;
            _doctorService = doctorService;
            _userService = userService;
            _doctorService = doctorService;

        }

        //COMMON VISITS
        public async Task<Visit> GetVisit(Guid visitId)
        {
            var doctorId = _userProviderService.GetCurrentUserId();
            var visit = await _context.Visits.SingleOrDefaultAsync(x => x.Id == visitId);

            if (visit != null)
            {
                visit.Clinic = _context.Clinics.FirstOrDefault(x => x.Id == visit.ClinicId);
                visit.Patient = await _userService.GetUserByIdAsync(visit.PatientId);
                visit.Doctor = await _doctorService.GetDoctor(visit.DoctorId);
            }

            return visit;
        }


        public async Task<List<Visit>> GetAllActiveVisits()
        {
            return await _context.Visits.Where(x => x.IsActive).ToListAsync();
        }

        public async Task<List<Visit>> GetInactiveVisits()
        {
            return await _context.Visits.Where(x => !x.IsActive).ToListAsync();
        }

        public async Task<List<Visit>> GetIncomingActiveVisits()
        {
            var visits = await _context.Visits
               .Where(visit => visit.VisitDate > DateTime.Now && visit.IsActive)
               .ToListAsync();

            return visits;
        }

        public async Task<List<Visit>> GetPreviousActiveVisits()
        {
            var visits = await _context.Visits
               .Where(visit => visit.VisitDate <= DateTime.Now && visit.IsActive)
               .ToListAsync();

            return visits;
        }

        public async Task CancelVisit(Guid visitId)
        {
            var visit = await GetVisit(visitId);
            visit.IsActive = false;
            await _context.SaveChangesAsync();
        }



        public async Task AddVisitByUser(CreateVisitDto visitDto)
        {
            var patientId = _userProviderService.GetCurrentUserId();
            var patient = await _userService.GetUserByIdAsync(Guid.Parse(patientId));
            var doctor = await _doctorService.GetDoctor(visitDto.DoctorId.Value);
            var doctorAdmission = await _doctorService.GetAdmissionByClinicAndDoctor(visitDto.DoctorId.Value, visitDto.ClinicId.Value);

            var newVisit = new Visit
            {
                PatientId = patient.Id,
                VisitDate = visitDto.VisitDate.Value.ToLocalTime(),
                DoctorId = visitDto.DoctorId.Value,
                IsActive = true,
                Fee = doctorAdmission.ConsultationFee,
                Patient = patient,
                Doctor = doctor,
                ClinicId = visitDto.ClinicId.Value,
                Description = visitDto.Description
            };

            _context.Visits.Add(newVisit);
            await _context.SaveChangesAsync();
        }

        //PATIENT VISITS
        public async Task<List<Visit>> GetUserIncomingVisits(int page, int pageSize, string firstName = null, string lastName = null, string pesel = null, string sortBy = null)
        {
            var patientId = _userProviderService.GetCurrentUserId();
            var query = _context.Visits.Where(visit => visit.VisitDate > DateTime.Now && visit.PatientId.ToString() == patientId );

            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(visit => visit.Patient.FirstName.Contains(firstName));
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(visit => visit.Patient.LastName.Contains(lastName));
            }
            if (!string.IsNullOrEmpty(pesel))
            {
                query = query.Where(visit => visit.Patient.Pesel.Contains(pesel));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {

                switch (sortBy.ToLower())
                {
                    case "date":
                        query = query.OrderBy(visit => visit.VisitDate);
                        break;
                    case "price":
                        query = query.OrderBy(visit => visit.Fee);
                        break;
                }
            }

            var visits = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            foreach (var visit in visits)
            {
                visit.Clinic =  _context.Clinics.FirstOrDefault(x => x.Id == visit.ClinicId);
                visit.Patient = await _userService.GetUserByIdAsync(visit.PatientId);
                visit.Doctor = await _doctorService.GetDoctor(visit.DoctorId);
            }

            return visits;
        }

        public async Task<List<Visit>> GetUserPreviousVisits(int page, int pageSize, string firstName = null, string lastName = null, string pesel = null, string sortBy = null)
        {
            var patientId = _userProviderService.GetCurrentUserId();
            var query =  _context.Visits
               .Where(visit => visit.VisitDate <= DateTime.Now && visit.PatientId.ToString() == patientId );

            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(visit => visit.Patient.FirstName.Contains(firstName));
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(visit => visit.Patient.LastName.Contains(lastName));
            }
            if (!string.IsNullOrEmpty(pesel))
            {
                query = query.Where(visit => visit.Patient.Pesel.Contains(pesel));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {

                switch (sortBy.ToLower())
                {
                    case "date":
                        query = query.OrderBy(visit => visit.VisitDate);
                        break;
                    case "price":
                        query = query.OrderBy(visit => visit.Fee);
                        break;
                }
            }

            var visits = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            foreach (var visit in visits)
            {
                visit.Clinic =  _context.Clinics.FirstOrDefault(x => x.Id == visit.ClinicId);
                visit.Patient = await _userService.GetUserByIdAsync(visit.PatientId);
                visit.Doctor = await _doctorService.GetDoctor(visit.DoctorId);
            }

            return visits;
        }

        //DOCTOR VISITS
        public async Task<List<Visit>> GetDoctorIncomingVisits(int page, int pageSize, string firstName = null, string lastName = null, string pesel = null, string sortBy = null)
        {
            var doctorId = _userProviderService.GetCurrentUserId();
            var query = _context.Visits.Where(visit => visit.VisitDate > DateTime.Now && visit.DoctorId.ToString() == doctorId);

            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(visit => visit.Patient.FirstName.Contains(firstName));
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(visit => visit.Patient.LastName.Contains(lastName));
            }
            if (!string.IsNullOrEmpty(pesel))
            {
                query = query.Where(visit => visit.Patient.Pesel.Contains(pesel));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {

                switch (sortBy.ToLower())
                {
                    case "date":
                        query = query.OrderBy(visit => visit.VisitDate);
                        break;
                    case "price":
                        query = query.OrderBy(visit => visit.Fee);
                        break;
                }
            }

            var visits = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            foreach (var visit in visits)
            {
                visit.Clinic =  _context.Clinics.FirstOrDefault(x => x.Id == visit.ClinicId);
                visit.Patient = await _userService.GetUserByIdAsync(visit.PatientId);
                visit.Doctor = await _doctorService.GetDoctor(visit.DoctorId);
            }

            return visits;
        }
        public async Task<List<Visit>> GetDoctorPreviousVisits(int page, int pageSize, string firstName = null, string lastName = null, string pesel = null, string sortBy = null)
        {
            var doctorId = _userProviderService.GetCurrentUserId();
            var query = _context.Visits
                .Where(visit => visit.VisitDate <= DateTime.Now && visit.DoctorId.ToString() == doctorId);

            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(visit => visit.Patient.FirstName.Contains(firstName));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(visit => visit.Patient.LastName.Contains(lastName));
            }

            if (!string.IsNullOrEmpty(pesel))
            {
                query = query.Where(visit => visit.Patient.Pesel.Contains(pesel));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {

                switch (sortBy.ToLower())
                {
                    case "date":
                        query = query.OrderBy(visit => visit.VisitDate);
                        break;
                    case "price":
                        query = query.OrderBy(visit => visit.Fee);
                        break;
                }
            }

            var visits = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            foreach (var visit in visits)
            {
                visit.Clinic = _context.Clinics.FirstOrDefault(x => x.Id == visit.ClinicId);
                visit.Patient = await _userService.GetUserByIdAsync(visit.PatientId);
                visit.Doctor = await _doctorService.GetDoctor(visit.DoctorId);
            }

            return visits;
        }


        public async Task<List<GetVisitsAppointmentDto>> GetVisitAppointments(int page = 1, int pageSize = 10, string firstName = null, string lastName = null, string address = null, string clinicName = null,  string sortBy = null)
        {
            var doctors = await  _context.Doctors.Include(x => x.ClinicDoctors).ToListAsync();
            var clinics = await _context.Clinics.ToListAsync();
            List<GetVisitsAppointmentDto> result = new List<GetVisitsAppointmentDto>();

            foreach (var doctor in doctors)
            {
                if (doctor.ClinicDoctors != null && doctor.ClinicDoctors.Any())
                {
                    foreach (var clinicDoctor in doctor.ClinicDoctors)
                    {
                        var clinic = clinics.FirstOrDefault(c => c.Id.Value == clinicDoctor.ClinicId);
                        var doctorAdmission = await _context.DoctorAdmissionConditions
                            .FirstOrDefaultAsync(x => x.ClinicId.Value == clinicDoctor.ClinicId && x.DoctorId.Value == clinicDoctor.DoctorId);

                        if (doctorAdmission != null)
                        {
                            var dto = new GetVisitsAppointmentDto
                            {
                                DoctorFirstName = doctor.FirstName,
                                DoctorLastName = doctor.LastName,
                                ClinicName = clinic.Name,
                                ClinicAddress = clinic.Address,
                                Fee = doctorAdmission.ConsultationFee ?? 0,
                                Specialization = doctorAdmission.Specialization,
                                ClosestDate = await GetNextAvailableDateTimeForPatient(doctor.Id, doctorAdmission.WorkHoursStart.Value, doctorAdmission.WorkHoursEnd.Value, doctorAdmission.WorkingDays),
                                ClinicId = clinic.Id,
                                DoctorId = doctor.Id,
                                DoctorAdmissionId = doctorAdmission.Id,
                            };

                            result.Add(dto);
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                result = result.Where(x => !string.IsNullOrEmpty(x.DoctorFirstName) && x.DoctorFirstName.Contains(firstName)).ToList();
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                result = result.Where(x => !string.IsNullOrEmpty(x.DoctorLastName) && x.DoctorLastName.Contains(lastName)).ToList();
            }

            if (!string.IsNullOrEmpty(clinicName))
            {
                result = result.Where(x => !string.IsNullOrEmpty(x.ClinicName) && x.ClinicName.Contains(clinicName)).ToList();
            }

            if (!string.IsNullOrEmpty( address))
            {
                result = result.Where(x => !string.IsNullOrEmpty(x.ClinicAddress) && x.ClinicAddress.Contains(address)).ToList();
            }


            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "date":
                        result = result.OrderBy(x => x.ClosestDate.Value).ToList();
                        break;
                    case "price":
                        result = result.OrderBy(x => x.Fee.Value).ToList();
                        break;
                }
            }

            result = result.Skip((page - 1) * pageSize)
                           .Take(pageSize)
                           .ToList();

            return result;
        }

        public async Task<DateTime?> GetNextAvailableDateTimeForPatient(Guid doctorId, DateTime workHoursStart, DateTime workHoursEnd, List<DayOfWeek>? acceptedDaysOfWeek)
        {
            var doctorVisits = await _context.Visits
                .Where(visit => visit.DoctorId == doctorId && visit.VisitDate > DateTime.Now)
                .OrderByDescending(visit => visit.VisitDate)
                .ToListAsync();

            DateTime lastVisitEndTime = DateTime.MinValue;

            if (doctorVisits.Any())
            {
                var latestVisit = doctorVisits.First();
                lastVisitEndTime = latestVisit.VisitDate.AddHours(1);
            }

            while (true)
            {
                if (lastVisitEndTime.TimeOfDay < workHoursStart.TimeOfDay)
                {
                    lastVisitEndTime = new DateTime(lastVisitEndTime.Year, lastVisitEndTime.Month, lastVisitEndTime.Day, workHoursStart.Hour, 0, 0);
                }

                if (lastVisitEndTime.TimeOfDay >= workHoursEnd.TimeOfDay)
                {
                    lastVisitEndTime = new DateTime(lastVisitEndTime.Year, lastVisitEndTime.Month, lastVisitEndTime.Day, workHoursStart.Hour, 0, 0).AddDays(1);
                }

                if (lastVisitEndTime > DateTime.Now && (acceptedDaysOfWeek?.Contains(lastVisitEndTime.DayOfWeek) ?? false))
                {
                    return lastVisitEndTime;
                }

                lastVisitEndTime = lastVisitEndTime.AddDays(1);
            }
        }

        public async Task<List<DateTime>> GetAllAvailableDateTimesForPatient(Guid doctorId, Guid clinicId)
        {
            var doctorAdmission = await _doctorService.GetAdmissionByClinicAndDoctor(doctorId, clinicId);

            if (doctorAdmission != null)
            {
                DateTime today = DateTime.Today;
                DateTime endOfMonth = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month), 23, 59, 59);

                var doctorVisits = await _context.Visits
                    .Where(visit => visit.DoctorId == doctorId && visit.VisitDate > today && visit.VisitDate < endOfMonth)
                    .OrderBy(visit => visit.VisitDate)
                    .ToListAsync();

                var availableDateTimes = new List<DateTime>();

                for (DateTime currentDate = today; currentDate <= endOfMonth; currentDate = currentDate.AddHours(1))
                {
                    if (currentDate.TimeOfDay >= doctorAdmission.WorkHoursStart.Value.TimeOfDay && currentDate.TimeOfDay < doctorAdmission.WorkHoursEnd.Value.TimeOfDay)
                    {
                        if (doctorAdmission.WorkingDays.Contains(currentDate.DayOfWeek))
                        {
                            // Sprawdź, czy obecna data i godzina są dostępne
                            if (!doctorVisits.Any(visit => visit.VisitDate.Year == currentDate.Year
                                                        && visit.VisitDate.Month == currentDate.Month
                                                        && visit.VisitDate.Day == currentDate.Day
                                                        && visit.VisitDate.Hour == currentDate.Hour))
                            {
                                availableDateTimes.Add(currentDate);
                            }
                        }
                    }
                }

                return availableDateTimes;
            }

            return new List<DateTime>();
        }

    }
}
