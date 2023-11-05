namespace API.Entities
{
    public class Visit
    {
            public Guid Id { get; set; }
            public DateTime VisitDate { get; set; }
            public Guid PatientId { get; set; }
            public User Patient { get; set; }
            public Guid DoctorId { get; set; }
            public Doctor Doctor { get; set; }
            public Guid ClinicId { get; set; }
            public Clinic Clinic { get; set; }
            public bool IsActive { get; set; }
            public decimal? Fee { get; set; }
            public string? Description { get; set; }
            public string? Reccomendations { get; set; }

    }
}
