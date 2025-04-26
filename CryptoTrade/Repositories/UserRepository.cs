using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.Interfaces.Repositories;
using CryptoTrade.Repositories.Interfaces;
using CryptoTrade.Services;

namespace CryptoTrade.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserRepository(AppDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<string> AuthenticateAsync(UserLoginDto userLoginDto)
        {
            var manager = GetService();
            return await manager.AuthenticateAsync(userLoginDto);
        }

        public async Task<User> CreateUserAsync(UserCreateDto userCreateDto)
        {
            var manager = GetService();
            return await manager.CreateUserAsync(userCreateDto);
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var manager = GetService();
            return await manager.DeleteUserAsync(id);
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            var manager = GetService();
            return await manager.GetUserByIdAsync(id);
        }

        public async Task<bool> UpdateUserAsync(UserUpdateDto userUpdateDto, string id)
        {
            var manager = GetService();
            return await manager.UpdateUserAsync(userUpdateDto, id);
        }

        private IUserServices GetService()
        {
            return new UserService(_context, _mapper,_configuration);
        }
    }
}
