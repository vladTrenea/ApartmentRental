using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Business.Core.Enums;
using ApartmentRentalWebApi.TestModelBuilders.Constants;

namespace ApartmentRentalWebApi.TestModelBuilders.Builders
{
	public class UserUpdateDtoBuilder
	{
		private string _firstName = UserTestConstants.ValidFirstName;

		private string _lastName = UserTestConstants.ValidLastName;

		private RoleEnum? _roleId = RoleEnum.Client;

		public UserUpdateDto Build()
		{
			return new UserUpdateDto
			{
				FirstName = _firstName,
				LastName = _lastName,
				RoleId = _roleId
			};
		}

		public UserUpdateDtoBuilder WithFirstName(string firstName)
		{
			_firstName = firstName;

			return this;
		}

		public UserUpdateDtoBuilder WithLastName(string lastName)
		{
			_lastName = lastName;

			return this;
		}

		public UserUpdateDtoBuilder WithRoleId(RoleEnum? roleId)
		{
			_roleId = roleId;

			return this;
		}
	}
}