using LapShop.MVC.Models;
using LapShop.MVC.Services;
using Microsoft.EntityFrameworkCore;

namespace LapShop.MVC;

public static class DependencyInjection
{
	public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
	{

		services.AddControllersWithViews();

		services.AddDatabaseConfig(configuration);

		services.AddScoped<ICategoryService , CategoryService>();
		services.AddScoped<IFileService, FileService>();
		services.AddScoped<IItemService, ItemService>();
		services.AddScoped<IItemTypeService, ItemTypeService>();
		services.AddScoped<IOsService, OsService>();




		return services;
	}


	private static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
	{

		var constr = configuration.GetConnectionString("constr") ??
			throw new InvalidOperationException("There is no Connection String For The 'DefaultConStr' Key ");

		services.AddDbContext<AppDBContext>(options =>
		{
			options.UseSqlServer(constr);
		});

		return services;
	}
}
