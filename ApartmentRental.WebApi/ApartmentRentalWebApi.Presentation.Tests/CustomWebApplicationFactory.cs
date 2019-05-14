using System;
using ApartmentRentalWebApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApartmentRentalWebApi.Presentation.Tests
{
	public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				var serviceProvider = new ServiceCollection()
					.AddEntityFrameworkInMemoryDatabase()
					.BuildServiceProvider();

				services.AddDbContext<ApartmentRentalDbContext>(options =>
				{
					options.UseInMemoryDatabase("InMemoryDbForTesting");
					options.UseInternalServiceProvider(serviceProvider);
				});

				var sp = services.BuildServiceProvider();
				using (var scope = sp.CreateScope())
				{
					var scopedServices = scope.ServiceProvider;
					var db = scopedServices.GetRequiredService<ApartmentRentalDbContext>();
					var logger = scopedServices
						.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

					// Ensure the database is created.
					db.Database.EnsureCreated();

					try
					{
						// Seed the database with test data.
						DbInitializer.Seed(db);
					}
					catch (Exception ex)
					{
						logger.LogError(ex, $"An error occurred seeding the " +
						                    "database with test messages. Error: {ex.Message}");
					}
				}
			});
		}
	}
}