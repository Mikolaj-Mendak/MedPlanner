
using API.Enums;

namespace API.Entities
{
    public class Doctor : User
    {
        public string? DoctorNumber { get; set; }
        public ICollection<Clinic>? ClinicWork { get; set; }
        public ICollection<DoctorAdmissionConditions>? AdmissionConditions { get; set; }
        public Guid? PhotoId { get; set; }
        public Photo Photo { get; set; }
        public bool IsClinicOwner { get; set; }
        public ICollection<ClinicDoctor>? ClinicDoctors { get; set; }
    }
}
