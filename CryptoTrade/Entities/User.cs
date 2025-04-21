using CryptoTrade.Entities.EntityRelatedEnums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTrade.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(UserName), IsUnique = true)]
    public class User
    {
        [Required, Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public Roles Role { get; set; } = Roles.Customer;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        [ForeignKey("Wallet")]
        public Guid WalletId { get; set; }
        public Wallet Wallet { get; set; } = new Wallet();
    }
}
