using BankingControlPanelAPI.Models.Dtos;

namespace BankingControlPanelAPI.Service.IService
{
    public interface IAuthService
    {
        Task<RegistrationResponseDto> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
