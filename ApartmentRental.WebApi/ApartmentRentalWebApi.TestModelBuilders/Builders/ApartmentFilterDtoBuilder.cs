using ApartmentRentalWebApi.Business.Core.Dto;

namespace ApartmentRentalWebApi.TestModelBuilders.Builders
{
	public class ApartmentFilterDtoBuilder
	{
		private double? _minPrice;
		private double? _maxPrice;
		private double? _minArea;
		private double? _maxArea;
		private int? _minNrRooms;
		private int? _maxNrRooms;
		private double _northEastLatitude;
		private double _northEastLongitude;
		private double _southWestLatitude;
		private double _southWestLongitude;

		public ApartmentFilterDto Build()
		{
			return new ApartmentFilterDto
			{
				MinPrice = _minPrice,
				MaxPrice = _maxPrice,
				MinArea = _minArea,
				MaxArea = _maxArea,
				MinNrRooms = _minNrRooms,
				MaxNrRooms = _maxNrRooms,
				NorthEastLatitude = _northEastLatitude,
				NorthEastLongitude = _northEastLongitude,
				SouthWestLatitude = _southWestLatitude,
				SouthWestLongitude = _southWestLongitude
			};
		}

		public ApartmentFilterDto Builder()
		{
			return new ApartmentFilterDto
			{
				MinPrice = _minPrice,
				MaxPrice = _maxPrice,
				MinArea = _minArea,
				MaxArea = _maxArea,
				MinNrRooms = _minNrRooms,
				MaxNrRooms = _maxNrRooms,
				NorthEastLatitude = _northEastLatitude,
				NorthEastLongitude = _northEastLongitude,
				SouthWestLatitude = _southWestLatitude,
				SouthWestLongitude = _southWestLongitude
			};
		}

		public ApartmentFilterDtoBuilder WithMinPrice(double? minPrice)
		{
			_minPrice = minPrice;

			return this;
		}

		public ApartmentFilterDtoBuilder WithMaxPrice(double? maxPrice)
		{
			_maxPrice = maxPrice;

			return this;
		}

		public ApartmentFilterDtoBuilder WithMinArea(double? minArea)
		{
			_minArea = minArea;

			return this;
		}

		public ApartmentFilterDtoBuilder WithMaxArea(double? maxArea)
		{
			_maxArea = maxArea;

			return this;
		}

		public ApartmentFilterDtoBuilder WithMinNrOfRooms(int? minNrOfRooms)
		{
			_minNrRooms = minNrOfRooms;

			return this;
		}

		public ApartmentFilterDtoBuilder WithMaxNrOfRooms(int? maxNrOfRooms)
		{
			_maxNrRooms = maxNrOfRooms;

			return this;
		}

		public ApartmentFilterDtoBuilder WithNorthEastLatitude(double northEastLatitude)
		{
			_northEastLatitude = northEastLatitude;

			return this;
		}

		public ApartmentFilterDtoBuilder WithNorthEastLongitude(double northEastLongitude)
		{
			_northEastLongitude = northEastLongitude;

			return this;
		}

		public ApartmentFilterDtoBuilder WithSouthWestLatitude(double southWestLatitude)
		{
			_southWestLatitude = southWestLatitude;

			return this;
		}

		public ApartmentFilterDtoBuilder WithSouthWestLongitude(double southWestLongitude)
		{
			_southWestLongitude = southWestLongitude;

			return this;
		}
	}
}