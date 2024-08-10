using BankingControlPanelAPI.Data;
using BankingControlPanelAPI.Models;
using BankingControlPanelAPI.Service.IService;
using BankingControlPanelAPI.Util;

namespace BankingControlPanelAPI.Service
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _appDbContext;
        public ClientService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;   
        }

        public async Task AddClient(Client client)
        {
            if (client.Accounts == null || client.Accounts.Count() == 0)
            {
                throw new BusinessLogicException("At least one account is required.");
            }

            await _appDbContext.Clients.AddAsync(client);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
