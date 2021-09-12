using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace day9.API
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			await CreateHostBuilder(args).Build().RunAsync();
		}

		private static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseUrls("https://*:5001");
					webBuilder.UseStartup<Startup>();
				});
	}
}
