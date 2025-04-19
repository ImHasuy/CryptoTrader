using CryptoTrade.Context;
using CryptoTrade.Entities;
using CryptoTrade.Repositories.Interfaces;

namespace CryptoTrade.Services
{
    public class UserService : IUserServicecs
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;

        }


        public Task<bool> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
