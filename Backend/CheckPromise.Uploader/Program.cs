using Checkpromise.Provider;
using CheckPromise.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CheckPromise.Uploader
{
	class Program
	{

		private static IConfigurationRoot Configuration { get; set; }

		private static void BuildConfiguration() {
			Configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", true, true)
				.Build();
		}

		private static void AddDbContext(ServiceCollection serviceCollection) {
			serviceCollection.AddDbContext<CheckPromiseContext>(options =>
					options
						.UseSqlServer(
							Configuration["ConnectionStrings:DefaultConnection"],
							sqlServerOptions => sqlServerOptions.CommandTimeout(300))
						.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning)));
		}

		private static ServiceProvider BuildServiceProvider() {
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddScoped<ClientDataUploader>();
			serviceCollection.AddScoped<IClientDataProvider, ClientDataFtpProvider>();
			AddDbContext(serviceCollection);
			return serviceCollection.BuildServiceProvider();
		}

		private static void InitializeDb(ServiceProvider serviceProvider)
		{
			CheckPromiseContext context = serviceProvider.GetRequiredService<CheckPromiseContext>();
			if (!context.Database.EnsureCreated()) {
				context.Database.Migrate();
			}
		}

		static void Main(string[] args)
		{
			BuildConfiguration();

			using (var serviceProvider = BuildServiceProvider()) {
				InitializeDb(serviceProvider);

				var clientDataUploader = serviceProvider.GetService<ClientDataUploader>();
				clientDataUploader.UploadData();
			}
		}
	}
}
