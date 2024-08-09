using System.ComponentModel.DataAnnotations;

namespace BankingControlPanelAPI.Models.Dtos
{
    public class RegistrationRequestDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression("^(Admin|User)$", ErrorMessage = "Role must be either Admin or User.")]
        public string Role { get; set; }
    }
}
