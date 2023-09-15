using API.Entities;

namespace API.Dtos
{
    public class OwnerDto : UserDto
    {
        public ICollection<Clinic>? Clinic { get; set; }
    }
}
