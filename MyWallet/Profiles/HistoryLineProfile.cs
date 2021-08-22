using AutoMapper;
using MyWallet.Models;
using MyWallet.Services.Dtos;

namespace MyWallet.Profiles
{
    public class HistoryLineProfile : Profile
    {
        public HistoryLineProfile()
        {
            CreateMap<Category, CategoryViewModel>()
                .ForMember(x => x.Name, x => x.MapFrom(o => o.Name))
                .ForMember(x => x.Color, x => x.MapFrom(o => o.Color));

            CreateMap<HistoryLine, HistoryLineModel>()
                .ForMember(x => x.Category, x => x.MapFrom(o => o.Category));
        }
    }
}
