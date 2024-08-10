using BankingControlPanelAPI.Util;
using System.ComponentModel.DataAnnotations;

namespace BankingControlPanelAPI.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required]
        [MaxLength(60)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(60)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(11)]
        public string PersonalId { get; set; }

        public string? ProfilePhoto { get; set; }

        [Required]
        [MaxLength(50)]
        public string MobileNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string Sex { get; set; }

        public Address? Address { get; set; }

        public IEnumerable<Account> Accounts { get; set; } = new List<Account>();
    }
}
