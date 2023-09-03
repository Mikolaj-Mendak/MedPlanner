namespace API.Entities
{
    public class DoctorAdmissionConditions
    {
        public Guid? Id { get; set; }
        public Guid? ClinicId { get; set; }
        public Guid? DoctorId { get; set; }
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
