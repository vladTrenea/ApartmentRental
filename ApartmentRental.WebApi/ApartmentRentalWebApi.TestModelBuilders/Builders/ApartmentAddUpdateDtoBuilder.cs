using System;

using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.TestModelBuilders.Constants;

namespace ApartmentRentalWebApi.TestModelBuilders.Builders
{
	public class ApartmentAddUpdateDtoBuilder
	{
		private string _title = ApartmentTestConstants.ValidTitle;
		private string _description = ApartmentTestConstants.ValidDescription;
		private double _area = ApartmentTestConstants.ValidArea;
		private double _pricePerMonth = ApartmentTestConstants.ValidPricePerMonth;
		private int _nrOfRooms = ApartmentTestConstants.ValidNrOfRooms;
		private double _latitude = ApartmentTestConstants.ValidLatitude;
		private double _longitude = ApartmentTestConstants.ValidLongitude;
		private readonly bool _isRented = true;
		private Guid? _realtorId;

		public ApartmentAddUpdateDto Build()
		{
			return new ApartmentAddUpdateDto
			{
				Title = _title,
				Description = _description,
				Area = _area,
				NrOfRooms = _nrOfRooms,
				PricePerMonth = _pricePerMonth,
				IsRented = _isRented,
				Latitude = _latitude,
				Longitude = _longitude,
				RealtorId = _realtorId
			};
		}

		public ApartmentAddUpdateDtoBuilder WithTitle(string title)
		{
			_title = title;

			return this;
		}

		public ApartmentAddUpdateDtoBuilder WithDescription(string description)
		{
			_description = description;

			return this;
		}

		public ApartmentAddUpdateDtoBuilder WithArea(double area)
		{
			_area = area;

			return this;
		}

		public ApartmentAddUpdateDtoBuilder WithPricePerMonth(double pricePerMonth)
		{
			_pricePerMonth = pricePerMonth;

			return this;
		}

		public ApartmentAddUpdateDtoBuilder WithNrOfRooms(int nrOfRooms)
		{
			_nrOfRooms = nrOfRooms;

			return this;
		}

		public ApartmentAddUpdateDtoBuilder WithLatitude(double latitude)
		{
			_latitude = latitude;

			return this;
		}

		public ApartmentAddUpdateDtoBuilder WithLongitude(double longitude)
		{
			_longitude = longitude;

			return this;
		}

		public ApartmentAddUpdateDtoBuilder WithRealtorId(Guid? realtorId)
		{
			_realtorId = realtorId;

			return this;
		}
	}
}