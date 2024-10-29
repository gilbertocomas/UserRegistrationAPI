using System.Net;

namespace UserRegistrationAPI.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Mensaje { get; set; } = new();
        public Object Result { get; set; }
    }
}
