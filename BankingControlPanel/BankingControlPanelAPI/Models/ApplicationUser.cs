using Microsoft.AspNetCore.Identity;

namespace BankingControlPanelAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
