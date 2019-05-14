using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApartmentRentalWebApi.Data
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddDataLayer(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<ApartmentRentalDbContext>(builder => { builder.UseSqlServer(connectionString); });

			return services;
		}
	}
}