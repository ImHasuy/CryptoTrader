using CryptoTrade.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CryptoTrade.Entities.EntityRelatedEnums;

namespace CryptoTrade.DTOs
{
    public class UserCreateDto
    {
        public string UserName { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class UserLoginDto
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class UserGetDto
    {
        public string UserName { get; set; } = string.Empty;
        public Guid WalletId { get; set; }
    }

}
