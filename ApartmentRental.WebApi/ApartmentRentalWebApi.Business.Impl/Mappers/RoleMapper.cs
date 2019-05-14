using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Domain.Entities;

namespace ApartmentRentalWebApi.Business.Impl.Mappers
{
	public static class RoleMapper
	{
		public static RoleDto ToRoleDto(Role role)
		{
			if (role == null)
			{
				return null;
			}

			return new RoleDto
			{
				Id = role.Id,
				Name = role.Name
			};
		}
	}
}