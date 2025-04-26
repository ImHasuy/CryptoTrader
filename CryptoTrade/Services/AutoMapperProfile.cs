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
            CreateMap<User, UserCreateDto>().ReverseMap()
                .ForMember(dest=> dest.Password, opt=>opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)))//Encrypts the password
                ; 

            //CreateTradeCrypto config
            CreateMap<CryptoTradeDTOtoFunc, CryptoTradeDTO>().ReverseMap();

            //Wallet config
            CreateMap<Wallet, WalletGetDto>().ReverseMap();

            //CryptoWallet config
            CreateMap<CryptoWallet, CryptoWalletGetDto>()
                .ForMember(dest => dest.CryptoName, opt => opt.MapFrom(src => src.Crypto.Name));

            //Crypto config
            CreateMap<Crypto, CryptoDTO>().ReverseMap();

            //Portfolio config
            CreateMap<CryptoWallet, PortfolioDto>().ReverseMap();

        }
    }
}
