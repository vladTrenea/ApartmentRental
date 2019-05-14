using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ApartmentRentalWebApi.Business.Core.Dto;

namespace ApartmentRentalWebApi.Business.Core.Services
{
	public interface IUserService
	{
		Task<IEnumerable<UserDto>> GetUsers();

		Task<IEnumerable<RealtorDto>> GetRealtors();

		Task<UserDto> GetById(Guid id);

		Task AddUser(UserAddDto model);

		Task UpdateUser(Guid id, UserUpdateDto model);

		Task DeleteUser(Guid id);
	}
}