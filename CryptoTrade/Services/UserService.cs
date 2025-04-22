using AutoMapper;
using CryptoTrade.Context;
using CryptoTrade.DTOs;
using CryptoTrade.Entities;
using CryptoTrade.Entities.EntityRelatedEnums;
using CryptoTrade.Repositories.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CryptoTrade.Services
{
    public class UserService : IUserServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserService(AppDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;

        }


        public async Task<User> CreateUserAsync(UserCreateDto userCreateDto)
        {
            var user = _mapper.Map<User>(userCreateDto);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;

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

        public async Task<User?> GetUserByIdAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == id);
            if (user == null)
            {
                throw new Exception($"User with id {id} not found");
            }
            return user;


        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }




        public async Task<string> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await Authenticate(userLoginDto);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Hibás E-mail cím vagy jelszó!");
            }

            return await GenerateToken(user);
            
        }



        public async Task<User?> Authenticate(UserLoginDto userLoginDto)
        {
            bool ValidBool;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userLoginDto.Email);
            if (user != null)
            {
                ValidBool = BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password);
                return user; //User exists, password matches
            }
            else
            {
                return null;
            }
        }
        public async Task<string> GenerateToken(User user)
        {
            var id = await GetClaimsIdentity(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwrSettings:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var exp = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtSettings:ExpiresInDays"]));
            var token = new JwtSecurityToken(_configuration["JwtSettings:Issuer"], _configuration["JwtSettings:Audience"], id.Claims, expires: exp, signingCredentials: creds);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
        public Task<ClaimsIdentity> GetClaimsIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            return Task.FromResult(new ClaimsIdentity(claims, "Token"));
        }


    }
}
