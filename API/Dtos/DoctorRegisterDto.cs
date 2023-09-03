using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class DoctorRegisterDto : RegisterDto
    {
        [Required]
        public string DoctorNumber { get; set; }

    }
}
