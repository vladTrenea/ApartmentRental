using System;

namespace ApartmentRentalWebApi.Business.Core.Dto
{
	public class RealtorDto
	{
		public Guid Id { get; set; }

		public string Email { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }
	}
}