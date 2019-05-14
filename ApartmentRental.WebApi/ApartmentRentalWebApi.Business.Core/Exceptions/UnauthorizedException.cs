using System;

namespace ApartmentRentalWebApi.Business.Core.Exceptions
{
	public class UnauthorizedException : Exception
	{
		public UnauthorizedException()
		{
		}

		public UnauthorizedException(string message) : base(message)
		{
		}
	}
}