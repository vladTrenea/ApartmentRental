using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Tokens;

using ApartmentRentalWebApi.Business.Core.Enums;
using ApartmentRentalWebApi.Business.Core.Settings;
using ApartmentRentalWebApi.Business.Impl;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.Localization;
using ApartmentRentalWebApi.Presentation.Middlewares;
using ApartmentRentalWebApi.Presentation.Swagger;
using ApartmentRentalWebApi.Presentation.Utils.Jwt;

namespace ApartmentRentalWebApi.Presentation
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public AuthenticationSettings JwtSettings { get; set; }

		public EmailSettings EmailSettings { get; set; }

		public ClientAppSettings ClientAppSettings { get; set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton(Configuration);

			JwtSettings = new AuthenticationSettings();
			Configuration.GetSection("AuthenticationConfiguration").Bind(JwtSettings);
			services.AddSingleton(JwtSettings);

			EmailSettings = new EmailSettings();
			Configuration.GetSection("EmailConfiguration").Bind(EmailSettings);
			services.AddSingleton(EmailSettings);

			ClientAppSettings = new ClientAppSettings();
			Configuration.GetSection("ClientAppConfiguration").Bind(ClientAppSettings);
			services.AddSingleton(ClientAppSettings);

			services.AddLocalization();
			services.Configure<RequestLocalizationOptions>(options =>
			{
				CultureInfo[] supportedCultures = new[]
				{
					new CultureInfo("en"),
				};

				options.DefaultRequestCulture = new RequestCulture("en");
				options.SupportedCultures = supportedCultures;
				options.SupportedUICultures = supportedCultures;
			});

			services.AddCors();
			services.AddRouting(options => options.LowercaseUrls = true);

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ClockSkew = TimeSpan.Zero,
						ValidIssuer = JwtSettings.ValidIssuer,
						ValidAudiences = new[] {JwtSettings.ValidAudience},
						IssuerSigningKey = JwtSecurityKey.Create(JwtSettings.SigningKey),
					};
					options.RequireHttpsMetadata = false;
				});

			services.AddAuthorization(options =>
			{
				options.AddPolicy(JwtConstants.AdminPolicy, policy => policy.RequireRole(RoleEnum.Admin.ToString("d")));
				options.AddPolicy(JwtConstants.RealtorPolicy,
					policy => policy.RequireRole(new List<string>{RoleEnum.Realtor.ToString("d"), RoleEnum.Admin.ToString("d")}));
			});

			services.AddSwaggerDocumentation();

			services.AddLocalizationLayer();
			services.AddDataLayer(Configuration.GetConnectionString("AppRentalDb"));
			services.AddBusinessLayer();

			services.AddMvc()
				.AddJsonOptions(options =>
				{
					options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
					options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
					options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
				}).AddFluentValidation();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.Use(async (ctx, next) =>
			{
				await next();
				if (ctx.Response.StatusCode == 204)
				{
					ctx.Response.ContentLength = 0;
				}
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwaggerDocumentation();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseRequestLocalization();
			app.UseCors(builder => builder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());
			app.UseMiddleware<ErrorHandlingMiddleware>();
			app.UseAuthentication();
			app.UseMvcWithDefaultRoute();
		}
	}
}