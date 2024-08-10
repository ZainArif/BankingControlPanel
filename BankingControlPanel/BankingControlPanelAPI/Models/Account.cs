using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingControlPanelAPI.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        [MaxLength(50)]
        public string AccountNumber { get; set; }
        public double Balance { get; set; }

        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
    }
}
