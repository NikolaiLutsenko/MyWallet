using AutoMapper;
using MyWallet.Data.Entities;
using MyWallet.Services.Dtos;

namespace MyWallet.Services.Profiles
{
	class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			CreateMap<CategoryEntity, Category>()
				.ForMember(x => x.Name, x => x.MapFrom(o => o.Label))
				.ForMember(x => x.Parrent, x => x.MapFrom(o => o.Parrent))
				.ForMember(x => x.Child, x => x.MapFrom(o => o.Child));
		}
	}
}
