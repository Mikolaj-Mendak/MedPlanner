using API.Entities;

namespace API.Dtos
{
    public class DoctorUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DoctorNumber { get; set; }
        public string Pesel { get; set; }
    }
}
