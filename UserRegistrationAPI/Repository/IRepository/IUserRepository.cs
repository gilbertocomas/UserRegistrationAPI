using UserRegistrationAPI.Models;
using UserRegistrationAPI.Models.DTOs;

namespace UserRegistrationAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<bool> IsUniqueUserAsync(string email);
        Task<User> RegisterAsync(User user);
    }
}
