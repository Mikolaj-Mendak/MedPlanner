namespace API.Entities
{
    public class ClinicOwner : User
    {
        public ICollection<Clinic>? Clinic { get; set; }
    }
}
