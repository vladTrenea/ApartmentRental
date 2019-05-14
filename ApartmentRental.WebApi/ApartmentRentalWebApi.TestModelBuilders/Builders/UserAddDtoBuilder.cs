using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Business.Core.Enums;
using ApartmentRentalWebApi.TestModelBuilders.Constants;

namespace ApartmentRentalWebApi.TestModelBuilders.Builders
{
	public class UserAddDtoBuilder
	{
		private string _email = UserTestConstants.ValidEmail;
		private string _firstName = UserTestConstants.ValidFirstName;
		private string _lastName = UserTestConstants.ValidFirstName;
		private RoleEnum? _roleId = RoleEnum.Client;

		public UserAddDto Build()
		{
			return new UserAddDto
			{
				Email = _email,
				FirstName = _firstName,
				LastName = _lastName,
				RoleId = _roleId
			};
		}

		public UserAddDtoBuilder WithEmail(string email)
		{
			_email = email;

			return this;
		}

		public UserAddDtoBuilder WithFirstName(string firstName)
		{
			_firstName = firstName;

			return this;
		}

		public UserAddDtoBuilder WithLastName(string lastName)
		{
			_lastName = lastName;

			return this;
		}

		public UserAddDtoBuilder WithRoleId(RoleEnum? role)
		{
			_roleId = role;

			return this;
		}
	}
}