using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ApartmentRentalWebApi.Business.Core.Dto;

namespace ApartmentRentalWebApi.Business.Core.Services
{
	public interface IApartmentService
	{
		Task<IEnumerable<ApartmentDto>> GetFiltered(ApartmentFilterDto model);

		Task<IEnumerable<ApartmentDto>> GetFilteredRentable(ApartmentFilterDto model);

		Task<ApartmentDto> GetById(Guid id);

		Task Add(ApartmentAddUpdateDto model);

		Task Update(Guid id, ApartmentAddUpdateDto model);

		Task Delete(Guid id);
	}
}