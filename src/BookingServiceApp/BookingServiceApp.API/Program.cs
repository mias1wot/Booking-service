using BookingServiceApp.Infrastructure.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RouteServiceAPP.Infrastructure.DataInitialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookingServiceApp.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			bool reinitializeDbData = false;
			bool reinitializeRidesOnly = true;


			var webHost = CreateHostBuilder(args).Build();

			
			using (var scope = webHost.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var context = services.GetRequiredService<BookingServiceContext>();

				// Create DB if it doesn't exist
				if (!context.Database.GetService<IRelationalDatabaseCreator>().Exists())
				{
					try
					{
						context.Database.Migrate();
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Migration has failed: {ex.Message}.");
					}
				}


				// Clear data if required
				if (reinitializeDbData)
				{
					BookingServiceDbInitializer.ClearData(context, reinitializeRidesOnly);
				}


				// Write initial values to DB
				BookingServiceDbInitializer.InitializeData(context);
			}


			webHost.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
