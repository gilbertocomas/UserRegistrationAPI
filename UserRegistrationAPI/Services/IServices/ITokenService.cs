namespace UserRegistrationAPI.Services.IServices
{
    public interface ITokenService
    {
        string GenerateJwtToken(string email);
    }
}
