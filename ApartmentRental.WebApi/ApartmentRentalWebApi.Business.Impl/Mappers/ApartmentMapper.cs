using System;

using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Business.Impl.Utils;
using ApartmentRentalWebApi.Domain.Entities;

namespace ApartmentRentalWebApi.Business.Impl.Mappers
{
	public static class ApartmentMapper
	{
		public static ApartmentDto ToApartmentDto(Apartment apartment)
		{
			if (apartment == null)
			{
				return null;
			}

			return new ApartmentDto
			{
				Id = apartment.Id,
				Title = apartment.Title,
				Description = apartment.Description,
				Area = apartment.Area,
				NrOfRooms = apartment.NrOfRooms,
				PricePerMonth = apartment.PricePerMonth,
				IsRented = apartment.IsRented,
				Latitude = apartment.Latitude,
				Longitude =  apartment.Longitude,
				RealtorId = apartment.RealtorId,
				RealtorName = apartment.Realtor.FirstName + " " + apartment.Realtor.LastName
			};
		}

		public static Apartment ToApartment(ApartmentAddUpdateDto apartmentDto)
		{
			if (apartmentDto == null)
			{
				return null;
			}

			return new Apartment
			{
				Id = SecurityUtils.GenerateApartmentId(),
				Title = apartmentDto.Title,
				Description = apartmentDto.Description,
				Area = apartmentDto.Area,
				NrOfRooms = apartmentDto.NrOfRooms,
				PricePerMonth = apartmentDto.PricePerMonth,
				IsRented = apartmentDto.IsRented,
				Latitude = apartmentDto.Latitude,
				Longitude = apartmentDto.Longitude,
				RealtorId = apartmentDto.RealtorId.Value,
				CreatedAt = DateTime.Now
			};
		}
	}
}