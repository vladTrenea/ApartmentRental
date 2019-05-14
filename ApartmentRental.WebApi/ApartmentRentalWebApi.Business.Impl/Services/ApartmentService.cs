using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Business.Core.Enums;
using ApartmentRentalWebApi.Business.Core.Exceptions;
using ApartmentRentalWebApi.Business.Core.Services;
using ApartmentRentalWebApi.Business.Impl.Mappers;
using ApartmentRentalWebApi.Business.Impl.Utils;
using ApartmentRentalWebApi.Business.Impl.Validators;
using ApartmentRentalWebApi.Data;
using ApartmentRentalWebApi.Domain.Entities;
using ApartmentRentalWebApi.Localization.Resources;

namespace ApartmentRentalWebApi.Business.Impl.Services
{
	public class ApartmentService : IApartmentService
	{
		private readonly ApartmentRentalDbContext _dbContext;

		private readonly IErrorMessages _errorMessages;

		public ApartmentService(ApartmentRentalDbContext dbContext, IErrorMessages errorMessages)
		{
			_dbContext = dbContext;
			_errorMessages = errorMessages;
		}

		public async Task<IEnumerable<ApartmentDto>> GetFiltered(ApartmentFilterDto model)
		{
			var query = GetFilteredApartments(model);

			var apartments = await query.ToListAsync();

			return apartments.Select(ApartmentMapper.ToApartmentDto);
		}

		public async Task<IEnumerable<ApartmentDto>> GetFilteredRentable(ApartmentFilterDto model)
		{
			var query = GetFilteredApartments(model);
			query = query.Where(a => !a.IsRented);

			var apartments = await query.ToListAsync();

			return apartments.Select(ApartmentMapper.ToApartmentDto);
		}

		public async Task<ApartmentDto> GetById(Guid id)
		{
			var apartment = await _dbContext.Apartments
				.Include(a => a.Realtor)
				.AsNoTracking()
				.FirstOrDefaultAsync(a => a.Id == id);
			if (apartment == null)
			{
				throw new NotFoundException();
			}

			return ApartmentMapper.ToApartmentDto(apartment);
		}

		public async Task Add(ApartmentAddUpdateDto model)
		{
			var validator = new ApartmentValidator(_errorMessages);
			validator.Validate(model);

			var realtor = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == model.RealtorId && u.RoleId == (int) RoleEnum.Realtor);
			if (realtor == null)
			{
				throw new NotFoundException();
			}

			var newApartment = ApartmentMapper.ToApartment(model);
			await _dbContext.Apartments.AddAsync(newApartment);
			await _dbContext.SaveChangesAsync();
		}

		public async Task Update(Guid id, ApartmentAddUpdateDto model)
		{
			var validator = new ApartmentValidator(_errorMessages);
			validator.Validate(model);

			var apartment = await _dbContext.Apartments.FirstOrDefaultAsync(a => a.Id == id);
			if (apartment == null)
			{
				throw new NotFoundException();
			}

			var realtor = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == model.RealtorId && u.RoleId == (int)RoleEnum.Realtor);
			if (realtor == null)
			{
				throw new NotFoundException();
			}

			apartment.Title = model.Title;
			apartment.Description = model.Description;
			apartment.Area = model.Area;
			apartment.NrOfRooms = model.NrOfRooms;
			apartment.PricePerMonth = model.PricePerMonth;
			apartment.Latitude = model.Latitude;
			apartment.Longitude = model.Longitude;
			apartment.RealtorId = model.RealtorId.Value;
			apartment.IsRented = model.IsRented;

			await _dbContext.SaveChangesAsync();
		}

		public async Task Delete(Guid id)
		{
			var apartment = await _dbContext.Apartments.FirstOrDefaultAsync(u => u.Id == id);
			if (apartment == null)
			{
				throw new NotFoundException();
			}

			_dbContext.Apartments.Remove(apartment);
			await _dbContext.SaveChangesAsync();
		}

		private IQueryable<Apartment> GetFilteredApartments(ApartmentFilterDto filter)
		{
			var query = _dbContext.Apartments.Include(a => a.Realtor).AsQueryable();
			query = query.Where(a => GeographicUtils.IsPointWithin(filter.SouthWestLatitude, filter.SouthWestLongitude,
				filter.NorthEastLatitude, filter.NorthEastLongitude, a.Latitude, a.Longitude));

			if (filter.MinPrice.HasValue)
			{
				query = query.Where(a => a.PricePerMonth >= filter.MinPrice.Value);
			}

			if (filter.MaxPrice.HasValue)
			{
				query = query.Where(a => a.PricePerMonth <= filter.MaxPrice.Value);
			}

			if (filter.MinArea.HasValue)
			{
				query = query.Where(a => a.Area >= filter.MinArea);
			}

			if (filter.MaxArea.HasValue)
			{
				query = query.Where(a => a.Area <= filter.MaxArea);
			}

			if (filter.MinNrRooms.HasValue)
			{
				query = query.Where(a => a.NrOfRooms >= filter.MinNrRooms);
			}

			if (filter.MaxNrRooms.HasValue)
			{
				query = query.Where(a => a.NrOfRooms <= filter.MaxNrRooms);
			}

			return query.AsNoTracking();
		}
	}
}