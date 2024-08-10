using BankingControlPanelAPI.Models;

namespace BankingControlPanelAPI.Service.IService
{
    public interface IClientService
    {
        public Task AddClient(Client client); 
    }
}
