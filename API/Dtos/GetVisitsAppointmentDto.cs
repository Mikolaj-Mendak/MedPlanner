
namespace API.Dtos
{
    public class GetVisitsAppointmentDto
    {
        public decimal? Fee { get; set; }
        public DateTime? ClosestDate { get; set; }
        public string? DoctorFirstName { get; set; }
        public string? DoctorLastName { get; set; }
        public string? ClinicName { get; set; }
        public string? ClinicAddress { get; set; }
        public string? Specialization { get; set; }
        public Guid? ClinicId {get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? DoctorAdmissionId { get; set; }

    }
}
