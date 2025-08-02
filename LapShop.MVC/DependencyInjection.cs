using LapShop.MVC.Persistance;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace LapShop.MVC;

public static class DependencyInjection
{
	public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
	{

		services.AddControllersWithViews();

		services
			.AddSessionConfig()
			.AddDatabaseConfig(configuration);

		services.AddIdentity<ApplicationUser, ApplicationRole>()
				.AddEntityFrameworkStores<AppDBContext>()
					.AddDefaultTokenProviders(); // optional but common;

		services.ConfigureApplicationCookie(option =>
		{
			option.LoginPath = "/Auth/Register";
			option.AccessDeniedPath = "/Auth/AccessDenied";
			option.Cookie.Name = "Cookie";
			option.Cookie.HttpOnly = true;
			option.ExpireTimeSpan = TimeSpan.FromMinutes(800);
			option.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
			option.SlidingExpiration = true;
		});


		

		services.AddScoped<ICategoryService , CategoryService>();
		services.AddScoped<IFileService, FileService>();
		services.AddScoped<IItemService, ItemService>();
		services.AddScoped<IItemTypeService, ItemTypeService>();
		services.AddScoped<IOsService, OsService>();
		services.AddScoped<IItemImagesService, ItemImagesService>();


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

	private static IServiceCollection AddSessionConfig(this IServiceCollection services)
	{
		services.AddSession();
		services.AddHttpContextAccessor();
		services.AddDistributedMemoryCache(); // if any error happend at client , caching the session info at server not client

		return services;
	}


}
