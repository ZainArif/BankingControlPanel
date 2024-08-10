using BankingControlPanelAPI.Models;
using BankingControlPanelAPI.Models.Dtos;

namespace BankingControlPanelAPI.Service.IService
{
    public interface IClientService
    {
        public Task AddClient(ClientDto clientDto);
        public Task<PaginatedResponse<ClientDto>> GetClients(string searchParam, string sortBy, int page, int pageSize);
    }
}
