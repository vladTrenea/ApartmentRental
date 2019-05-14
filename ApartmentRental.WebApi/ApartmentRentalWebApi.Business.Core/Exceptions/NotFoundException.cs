using System;

namespace ApartmentRentalWebApi.Business.Core.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException()
		{
		}

		public NotFoundException(string message) : base(message)
		{
		}
	}
}