using AutoMapper;
using CryptoTrade.DTOs;
using CryptoTrade.Entities;

namespace CryptoTrade.Services
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            //User config
            CreateMap<User, UserCreateDto>().ReverseMap();
        }
    }
}
