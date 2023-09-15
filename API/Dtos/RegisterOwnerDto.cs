using API.Entities;

namespace API.Dtos
{
    public class RegisterOwnerDto : RegisterDto
    {
        public ICollection<Clinic>? Clinic { get; }
    }
}
