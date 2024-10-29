namespace UserRegistrationAPI.Models.DTOs
{
    public class UserCreateDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<PhoneDTO> Phones { get; set; }
    }
}
