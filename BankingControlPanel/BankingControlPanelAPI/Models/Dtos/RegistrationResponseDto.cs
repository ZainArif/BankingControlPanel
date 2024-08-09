namespace BankingControlPanelAPI.Models.Dtos
{
    public class RegistrationResponseDto
    {
        public bool IsUserRegistered { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
