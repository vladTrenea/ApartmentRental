using System;

namespace ApartmentRentalWebApi.Business.Core.Exceptions
{
	public class ConflictException : Exception
	{
		public ConflictException()
		{
		}

		public ConflictException(string message) : base(message)
		{
		}
	}
}