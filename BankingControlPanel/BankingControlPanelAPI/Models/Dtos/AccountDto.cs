using System.ComponentModel.DataAnnotations;

namespace BankingControlPanelAPI.Models.Dtos
{
    public class AccountDto
    {
        [MaxLength(50)]
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
    }
}
