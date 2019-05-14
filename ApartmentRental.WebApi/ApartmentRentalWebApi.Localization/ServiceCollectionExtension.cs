using ApartmentRentalWebApi.Localization.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace ApartmentRentalWebApi.Localization
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddLocalizationLayer(this IServiceCollection services)
		{
			services.AddTransient<IErrorMessages, ErrorMessages>();
			services.AddTransient<ILabelMessages, LabelMessages>();

			return services;
		}
	}
}