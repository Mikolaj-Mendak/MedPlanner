using API.Entities;

namespace API.Dtos
{
    public class AddClinicDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? NIP { get; set; }
        public bool? IsNFZ { get; set; }
        public bool? IsPrivate { get; set; }
        public ICollection<ClinicDoctor>? ClinicDoctors { get; set; }
        public DateTime? OfficeHoursStartDate { get; set; }
        public DateTime? OfficeHoursEndDate { get; set; }
        public List<int>? WorkingDays { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
