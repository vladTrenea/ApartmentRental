using System.Collections.Generic;
using System.Threading.Tasks;
using ApartmentRentalWebApi.Business.Core.Dto;

namespace ApartmentRentalWebApi.Business.Core.Services
{
	public interface IRoleService
	{
		Task<IEnumerable<RoleDto>> GetRoles();
	}
}