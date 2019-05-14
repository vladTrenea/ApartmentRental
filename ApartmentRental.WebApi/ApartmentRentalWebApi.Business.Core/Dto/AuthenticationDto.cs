using System;

using ApartmentRentalWebApi.Business.Core.Enums;

namespace ApartmentRentalWebApi.Business.Core.Dto
{
	public class AuthenticationDto
	{
		public string Token { get; set; }

		public Guid UserId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public RoleEnum RoleId { get; set; }
	}
}