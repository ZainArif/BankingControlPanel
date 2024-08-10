using System.ComponentModel.DataAnnotations;

namespace BankingControlPanelAPI.Models.Dtos
{
    public class AddressDto
    {
        [MaxLength(200)]
        public string Country { get; set; }
        [MaxLength(200)]
        public string City { get; set; }
        [MaxLength(400)]
        public string Street { get; set; }
        [MaxLength(100)]
        public string ZipCode { get; set; }
    }
}
