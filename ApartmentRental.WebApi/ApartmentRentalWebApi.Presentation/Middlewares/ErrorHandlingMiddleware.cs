using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Business.Core.Exceptions;

namespace ApartmentRentalWebApi.Presentation.Middlewares
{
	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		private readonly ILogger<ErrorHandlingMiddleware> _logger;

		public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context /* other scoped dependencies */)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex, _logger);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception,
			ILogger<ErrorHandlingMiddleware> logger)
		{
			int code;

			if (exception is ValidationException)
			{
				code = StatusCodes.Status400BadRequest;
			}
			else if (exception is UnauthorizedException)
			{
				code = StatusCodes.Status401Unauthorized;
			}
			else if (exception is ForbiddenException)
			{
				code = StatusCodes.Status403Forbidden;
			}
			else if (exception is NotFoundException)
			{
				code = StatusCodes.Status404NotFound;
			}
			else if (exception is ConflictException)
			{
				code = StatusCodes.Status409Conflict;
			}
			else
			{
				code = StatusCodes.Status500InternalServerError;
				logger.LogError("Internal server error", exception.Message);
			}

			var result = JsonConvert.SerializeObject(new ErrorDto(exception.Message),
				Formatting.Indented,
				new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = code;

			return context.Response.WriteAsync(result);
		}
	}
}