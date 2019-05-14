using ApartmentRentalWebApi.Business.Core.Enums;

namespace ApartmentRentalWebApi.Business.Core.Dto
{
	public class UserAddDto
	{
		public string Email { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public RoleEnum? RoleId { get; set; }
	}
}