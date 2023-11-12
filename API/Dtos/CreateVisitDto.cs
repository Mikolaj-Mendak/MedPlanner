namespace API.Dtos
{
    public class CreateVisitDto
    {
        public DateTime? VisitDate { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? ClinicId { get; set; }
        public string? Description { get; set; }
    }
}
