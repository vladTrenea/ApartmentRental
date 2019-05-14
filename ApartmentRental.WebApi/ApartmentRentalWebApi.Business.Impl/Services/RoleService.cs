using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Business.Core.Enums;
using ApartmentRentalWebApi.Business.Core.Services;
using ApartmentRentalWebApi.Business.Impl.Mappers;
using ApartmentRentalWebApi.Data;

namespace ApartmentRentalWebApi.Business.Impl.Services
{
	public class RoleService : IRoleService
	{
		private readonly ApartmentRentalDbContext _dbContext;

		public RoleService(ApartmentRentalDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<RoleDto>> GetRoles()
		{
			var roles = await _dbContext.Roles
				.Where(r => r.Id != (int)RoleEnum.Admin)
				.OrderBy(r => r.Name)
				.AsNoTracking()
				.ToListAsync();

			return roles.Select(RoleMapper.ToRoleDto);
		}
	}
}