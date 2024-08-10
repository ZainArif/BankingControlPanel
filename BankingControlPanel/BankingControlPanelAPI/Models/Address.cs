using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingControlPanelAPI.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        [MaxLength(200)]
        public string Country { get; set; }
        [MaxLength(200)]
        public string City { get; set; }
        [MaxLength(400)]
        public string Street { get; set; }
        [MaxLength(100)]
        public string ZipCode { get; set; }
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
    }
}
