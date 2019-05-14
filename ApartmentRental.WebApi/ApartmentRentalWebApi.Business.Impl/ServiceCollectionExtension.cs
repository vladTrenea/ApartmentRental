using Microsoft.Extensions.DependencyInjection;

using ApartmentRentalWebApi.Business.Core.Services;
using ApartmentRentalWebApi.Business.Impl.Services;

namespace ApartmentRentalWebApi.Business.Impl
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
		{
			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IHashService, HashService>(s => new HashService("SHA1"));
			services.AddScoped<IApartmentService, ApartmentService>();

			return services;
		}
	}
}