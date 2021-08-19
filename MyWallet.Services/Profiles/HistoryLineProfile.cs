using AutoMapper;
using MyWallet.Data.Entities;
using MyWallet.Services.Dtos;

namespace MyWallet.Services.Profiles
{
    class HistoryLineProfile : Profile
    {
        public HistoryLineProfile()
        {
            CreateMap<HistoryLineEntity, HistoryLine>()
                .ForMember(x => x.Type, x => x.MapFrom(o => o.Type.ToString()))
                .ForMember(x => x.Category, x => x.MapFrom(o => o.Category));
        }
    }
}
