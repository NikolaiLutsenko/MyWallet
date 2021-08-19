using Microsoft.Extensions.DependencyInjection;
using MyWallet.Services.Interfaces;
using MyWallet.Services.Profiles;

namespace MyWallet.Services.Extensions
{
	public static class DIExtensions
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IHistoryLinesService, HistoryLinesService>();
			services.AddAutoMapper(typeof(CategoryProfile).Assembly);
			return services;
		}
	}
}
