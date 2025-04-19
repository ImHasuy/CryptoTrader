using CryptoTrade.DTOs;
using CryptoTrade.Entities;

namespace CryptoTrade.Repositories.Interfaces
{
    public interface IUserServicecs
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> CreateUserAsync(UserCreateDto userCreateDto);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
