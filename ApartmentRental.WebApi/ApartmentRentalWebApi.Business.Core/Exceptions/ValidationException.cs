using System;

namespace ApartmentRentalWebApi.Business.Core.Exceptions
{
	public class ValidationException : Exception
	{
		public ValidationException()
		{
		}

		public ValidationException(string message) : base(message)
		{
		}
	}
}