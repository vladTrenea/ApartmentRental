using ApartmentRentalWebApi.Business.Core.Enums;

namespace ApartmentRentalWebApi.Business.Core.Dto
{
	public class UserUpdateDto
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public RoleEnum? RoleId { get; set; }
	}
}