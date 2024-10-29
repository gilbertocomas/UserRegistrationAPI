using System.Text.Json.Serialization;

namespace UserRegistrationAPI.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string CityCode { get; set; }
        public string CountryCode { get; set; }

        // Relación con User
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
