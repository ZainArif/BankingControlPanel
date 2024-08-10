using AutoMapper;
using BankingControlPanelAPI.Models;
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
        private IMapper _mapper;
        private ResponseDto _response;

        public ClientAPIController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] ClientDto model)
        {
            try
            {
                var client = _mapper.Map<Client>(model);

                await _clientService.AddClient(client);

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
    }
}
