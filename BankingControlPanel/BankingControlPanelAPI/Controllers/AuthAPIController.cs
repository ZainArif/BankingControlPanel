﻿using BankingControlPanelAPI.Models.Dtos;
using BankingControlPanelAPI.Service.IService;
using BankingControlPanelAPI.Util;
using Microsoft.AspNetCore.Mvc;

namespace BankingControlPanelAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private ResponseDto _response;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _response = new ResponseDto();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            try
            {
                var registrationResponse = await _authService.Register(model);

                _response.Message = registrationResponse.Message;

                if (!registrationResponse.IsUserRegistered)
                {
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                return Ok(_response);
            }
            catch (Exception ex)
            {
                return Common.GetExceptionResponse(ex);
            }
        }
    }
}
