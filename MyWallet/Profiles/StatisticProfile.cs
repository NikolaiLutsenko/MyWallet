using AutoMapper;
using MyWallet.Models;
using MyWallet.Services.Dtos;

namespace MyWallet.Profiles
{
    public class StatisticProfile : Profile
    {
        public StatisticProfile()
        {
            CreateMap<StatisticItem, StatisticItemModel>();
        }
    }
}
