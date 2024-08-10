using AutoMapper;
using BankingControlPanelAPI.Data;
using BankingControlPanelAPI.Models;
using BankingControlPanelAPI.Models.Dtos;
using BankingControlPanelAPI.Models.Helpers;
using BankingControlPanelAPI.Service.IService;
using BankingControlPanelAPI.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BankingControlPanelAPI.Service
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private const string LastFilterParamsCacheKey = "LastFilterParams";

        public ClientService(AppDbContext appDbContext, IMapper mapper, IMemoryCache memoryCache)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task AddClient(ClientDto clientDto)
        {
            if (clientDto.Accounts == null || clientDto.Accounts.Count() == 0)
            {
                throw new BusinessLogicException("At least one account is required.");
            }

            var client = _mapper.Map<Client>(clientDto);

            await _appDbContext.Clients.AddAsync(client);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<ClientDto>> GetClients(string userId, string searchParam, string sortBy, int page, int pageSize)
        {
            IQueryable<Client> query = _appDbContext.Clients
                                                    .Include(c => c.Address)
                                                    .Include(c => c.Accounts);

            if (!string.IsNullOrEmpty(searchParam))
            {
                query = query.Where(c => c.FirstName.Contains(searchParam) || c.LastName.Contains(searchParam) ||
                                         c.Email.Contains(searchParam) || c.PersonalId.Contains(searchParam) ||
                                         c.MobileNumber.Contains(searchParam)
                                   );
            }

            query = sortBy switch
            {
                "FirstName" => query.OrderBy(c => c.FirstName),
                "LastName" => query.OrderBy(c => c.LastName),
                "Email" => query.OrderBy(c => c.Email),
                _ => query.OrderBy(c => c.ClientId)
            };

            int totalValues = await query.CountAsync();
            int totalPages = (totalValues / pageSize) + (totalValues % pageSize == 0 ? 0 : 1);

            var clients = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            SaveLastFilterParams(new FilterParams()
            {
                UserId = userId,
                SearchParam = searchParam,
                SortBy = sortBy,
                Page = page,
                PageSize = pageSize
            });

            return new PaginatedResponse<ClientDto>
            {
                Values = _mapper.Map<IEnumerable<ClientDto>>(clients),
                TotalValues = totalValues,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,    
            };
        }

        private void SaveLastFilterParams(FilterParams filterParam)
        {
            var filterParams = _memoryCache.GetOrCreate(LastFilterParamsCacheKey, entry => new List<FilterParams>());

            if (filterParams.Count == 3)
                filterParams.RemoveAt(0);   

            filterParams.Add(filterParam);

            _memoryCache.Set(LastFilterParamsCacheKey, filterParams);
        }
    }
}
