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
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator; 
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

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var loginResponseDto = new LoginResponseDto();

            var user = _userManager.Users.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || isValid == false)
            {
                return loginResponseDto;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDto userDto = new()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            loginResponseDto.User = userDto;
            loginResponseDto.Token = token; 

            return loginResponseDto;
        }
    }
}
