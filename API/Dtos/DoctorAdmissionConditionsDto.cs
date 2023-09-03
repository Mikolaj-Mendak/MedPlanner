using API.Entities;

namespace API.Dtos
{
    public class DoctorAdmissionConditionsDto
    {
        public Guid? ClinicId { get; set; }
        public Doctor? Doctor { get; set; }
        public string? Specialization { get; set; }
        public bool? IsNFZ { get; set; }
        public bool? IsPrivate { get; set; }
        public decimal? ConsultationFee { get; set; }
        public List<DayOfWeek>? WorkingDays { get; set; }
        public DateTime? WorkHoursStart { get; set; }
        public DateTime? WorkHoursEnd { get; set; }
    }
}
