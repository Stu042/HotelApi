using HotelApi;
using HotelApi.Repositories;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

[assembly: FunctionsStartup(typeof(Startup))]
namespace HotelApi;


public class Startup : FunctionsStartup {
	public override void Configure(IFunctionsHostBuilder builder) {
		var executionContextOptions = builder.Services.BuildServiceProvider()
			.GetService<IOptions<Microsoft.Azure.WebJobs.Host.Bindings.ExecutionContextOptions>>().Value;
		var currentDirectory = executionContextOptions.AppDirectory;
		var config = new ConfigurationBuilder()
			.SetBasePath(currentDirectory)
			.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
			.AddEnvironmentVariables()
			.Build();
		var services = builder.Services;
		services.AddDbContextFactory<HotelDbContext>(options =>
			options.UseSqlServer(config.GetConnectionString("HotelDb")));
		services.AddScoped<IHotelRepo, HotelRepo>();
	}
}
