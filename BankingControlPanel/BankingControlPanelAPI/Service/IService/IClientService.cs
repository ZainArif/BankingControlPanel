using BankingControlPanelAPI.Models.Dtos;
using BankingControlPanelAPI.Models.Helpers;

namespace BankingControlPanelAPI.Service.IService
{
    public interface IClientService
    {
        public Task AddClient(ClientDto clientDto);
        public Task<PaginatedResponse<ClientDto>> GetClients(string userId, string searchParam, string sortBy, int page, int pageSize);
        public IEnumerable<FilterParams> GetLastFilterParams(string userId);
    }
}
