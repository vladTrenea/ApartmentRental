using System;

using ApartmentRentalWebApi.Business.Core.Enums;

namespace ApartmentRentalWebApi.Business.Core.Dto
{
	public class UserDto
	{
		public Guid Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public bool HasEmailConfirmed { get; set; }

		public RoleEnum RoleId { get; set; }

		public string RoleName { get; set; }
	}
}