using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using System.Security.Claims;

namespace CryptoTrade.Repositories.Interfaces
{
    public interface IUserServices
    {
        Task<string> AuthenticateAsync(UserLoginDto userLoginDto);
        Task<string> GenerateToken(User user);
        Task<ClaimsIdentity> GetClaimsIdentity(User user);
        Task<User?> GetUserByIdAsync(string id);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> CreateUserAsync(UserCreateDto userCreateDto);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> SaveChangesAsync();

    }
}
