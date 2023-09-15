using API.Entities;

namespace API.Dtos
{
    public class OwnerUpdateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Pesel { get; set; }
        public ICollection<Clinic>? Clinic { get; }


    }
}
