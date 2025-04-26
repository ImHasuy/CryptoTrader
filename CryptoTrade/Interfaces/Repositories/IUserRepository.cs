using CryptoTrade.DTOs;
using CryptoTrade.Entities;

namespace CryptoTrade.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UpdateUserAsync(UserUpdateDto userUpdateDto, string id);
        Task<bool> DeleteUserAsync(string id);
        Task<string> AuthenticateAsync(UserLoginDto userLoginDto);
        Task<User> CreateUserAsync(UserCreateDto userCreateDto);
        Task<User?> GetUserByIdAsync(string id);
    }
}
