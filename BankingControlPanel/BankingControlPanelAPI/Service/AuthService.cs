using BankingControlPanelAPI.Models;
using BankingControlPanelAPI.Models.Dtos;
using BankingControlPanelAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace BankingControlPanelAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager; 
        }

        public async Task<RegistrationResponseDto> Register(RegistrationRequestDto registrationRequestDto)
        {
            var registrationResponseDto = new RegistrationResponseDto();

            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber,
            };

            var userCreated = await _userManager.CreateAsync(user, registrationRequestDto.Password);

            if (userCreated.Succeeded)
            {
                var roleExist = await _roleManager.RoleExistsAsync(registrationRequestDto.Role);
                
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(registrationRequestDto.Role));
                }

                await _userManager.AddToRoleAsync(user, registrationRequestDto.Role);

                registrationResponseDto.IsUserRegistered = true;
                registrationResponseDto.Message = "User registered successfully.";
            }
            else
            {
                registrationResponseDto.IsUserRegistered = false;
                registrationResponseDto.Message = userCreated?.Errors?.FirstOrDefault()?.Description ?? "User registration failed.";
            }

            return registrationResponseDto;
        }
    }
}
