using AutoMapper;
using BankingControlPanelAPI.Models;
using BankingControlPanelAPI.Models.Dtos;

namespace BankingControlPanelAPI.Util
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => 
            { 
                config.CreateMap<ClientDto, Client>().ReverseMap();
                config.CreateMap<AddressDto, Address>().ReverseMap();
                config.CreateMap<AccountDto, Account>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
