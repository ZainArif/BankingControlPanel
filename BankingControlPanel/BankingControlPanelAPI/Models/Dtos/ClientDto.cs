using BankingControlPanelAPI.Util;
using System.ComponentModel.DataAnnotations;

namespace BankingControlPanelAPI.Models.Dtos
{
    public class ClientDto
    {
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
        [StringLength(11, MinimumLength = 11)]
        public string PersonalId { get; set; }

        public string? ProfilePhoto { get; set; }

        [Required]
        [MaxLength(50)]
        [MobileNumber]
        public string MobileNumber { get; set; }

        [Required]
        [RegularExpression("^(Male|Female)$", ErrorMessage = "Sex must be either Male or Female.")]
        public string Sex { get; set; }

        public AddressDto? Address { get; set; }

        public IEnumerable<AccountDto> Accounts { get; set; } = new List<AccountDto>();
    }
}
