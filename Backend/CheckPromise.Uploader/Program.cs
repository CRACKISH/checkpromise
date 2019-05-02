using Checkpromise.Provider;
using Microsoft.Extensions.DependencyInjection;

namespace CheckPromise.Uploader
{
	class Program
	{

		private static ServiceProvider BuildServiceProvider() {
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddScoped<ClientDataUploader>();
			serviceCollection.AddScoped<IClientDataProvider, ClientDataFtpProvider>();
			return serviceCollection.BuildServiceProvider();
		}

		static void Main(string[] args)
		{
			using (var serviceProvider = BuildServiceProvider()) {
				var clientDataUploader = serviceProvider.GetService<ClientDataUploader>();
				clientDataUploader.UploadData();
			}
		}
	}
}
