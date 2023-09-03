namespace API.Entities
{
    public class ClinicDoctor
    {
        public Guid ClinicId { get; set; }
        public Clinic Clinic { get; set; }

        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
