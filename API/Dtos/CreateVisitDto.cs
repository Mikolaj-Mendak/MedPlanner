namespace API.Dtos
{
    public class CreateVisitDto
    {
        public DateTime VisitDate { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public decimal Fee { get; set; }
    }
}
