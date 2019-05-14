using ApartmentRentalWebApi.Business.Core.Enums;
using ApartmentRentalWebApi.Domain.Entities;

namespace ApartmentRentalWebApi.TestModelBuilders.Builders
{
	public class RoleBuilder
	{
		private int _id;
		private string _name;

		public Role Build()
		{
			return new Role
			{
				Id = _id,
				Name = _name
			};
		}

		public RoleBuilder WithRole(RoleEnum role)
		{
			this._id = (int)role;
			this._name = role.ToString();

			return this;
		}
	}
}