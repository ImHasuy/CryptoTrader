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
            var EmailValid = await _context.Users.FirstOrDefaultAsync(u => u.Email == userCreateDto.Email);
            var UserNameValid = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userCreateDto.UserName);
            if (EmailValid != null)
            {
                throw new Exception($"User with email {userCreateDto.Email} already exists");
            }
            if(UserNameValid != null)
            {
                throw new Exception($"User with username {userCreateDto.UserName} already exists");
            }

            var user = _mapper.Map<User>(userCreateDto);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;

        }
        public async Task<User> CreateAdminAsync(UserCreateDto userCreateDto)
        {
            var EmailValid = await _context.Users.FirstOrDefaultAsync(u => u.Email == userCreateDto.Email);
            var UserNameValid = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userCreateDto.UserName);
            if (EmailValid != null)
            {
                throw new Exception($"User with email {userCreateDto.Email} already exists");
            }
            if (UserNameValid != null)
            {
                throw new Exception($"User with username {userCreateDto.UserName} already exists");
            }

            var user = _mapper.Map<User>(userCreateDto);
            user.Role = Roles.Admin;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;

        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await GetUserByIdAsync(id);
            user.IsEnabled = false;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByIdAsync(string id)
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
     
        public async Task<bool> UpdateUserAsync(UserUpdateDto userUpdateDto, string id)
        {
            var user = await GetUserByIdAsync(id);
            if (!string.IsNullOrEmpty(userUpdateDto.UserName))
            {
                user.UserName = userUpdateDto.UserName;
            }
            if (!string.IsNullOrEmpty(userUpdateDto.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(userUpdateDto.Password);
            }
            if (!string.IsNullOrEmpty(userUpdateDto.Email))
            {
                user.Email = userUpdateDto.Email;
            }
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }


        /// <summary>
        /// Authenticates a user based on the provided login credentials.
        /// </summary>
        /// <param name="userLoginDto">User Login details</param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<string> AuthenticateAsync(UserLoginDto userLoginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userLoginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password) || user.IsEnabled == false)
            {
                throw new UnauthorizedAccessException("Hibás E-mail cím vagy jelszó!");
            }
            
            return await GenerateToken(user);
        }

        /// <summary>
        /// Generates a JWT token for the user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> GenerateToken(User user)
        {
            var id = await GetClaimsIdentity(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var exp = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiresInMinutes"]));
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
