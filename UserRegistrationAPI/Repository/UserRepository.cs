using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserRegistrationAPI.Data;
using UserRegistrationAPI.Models;
using UserRegistrationAPI.Models.DTOs;
using UserRegistrationAPI.Repository.IRepository;

namespace UserRegistrationAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context; 
        private readonly IMapper _mapper;
        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> IsUniqueUserAsync(string email)
        {
            var userDB = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

            if(userDB is null)
            {
                return true;
            }

            return false;
        }

        public async Task<User> RegisterAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user; 
        }
    }
}
