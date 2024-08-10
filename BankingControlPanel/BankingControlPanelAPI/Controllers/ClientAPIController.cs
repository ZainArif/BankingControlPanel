using BankingControlPanelAPI.Models.Dtos;
using BankingControlPanelAPI.Service.IService;
using BankingControlPanelAPI.Util;
using Microsoft.AspNetCore.Mvc;

namespace BankingControlPanelAPI.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientAPIController : ControllerBase
    {
        private readonly IClientService _clientService;
        private ResponseDto _response;

        public ClientAPIController(IClientService clientService)
        {
            _clientService = clientService;
            _response = new ResponseDto();
        }

        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] ClientDto model)
        {
            try
            {
                await _clientService.AddClient(model);

                _response.Message = "Client added successfully.";

                return Ok(_response);
            }
            catch (BusinessLogicException blex)
            {
                _response.IsSuccess = false;
                _response.Message = blex.Message;

                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                return Common.GetExceptionResponse(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetClients([FromQuery] string? searchParam, [FromQuery] string? sortBy, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var clients = await _clientService.GetClients(searchParam, sortBy, page, pageSize);

                _response.Result = clients;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                return Common.GetExceptionResponse(ex);
            }
        }
    }
}
