using FluentValidation;

using ApartmentRentalWebApi.Business.Core.Dto;
using ApartmentRentalWebApi.Localization.Resources;

namespace ApartmentRentalWebApi.Business.Impl.Validators
{
	public class ApartmentValidator : BaseValidator<ApartmentAddUpdateDto>
	{
		public ApartmentValidator(IErrorMessages errorMessages)
		{
			RuleFor(model => model.Title)
				.NotEmpty()
				.WithMessage(errorMessages.TitleRequired);

			RuleFor(model => model.Description)
				.NotEmpty()
				.WithMessage(errorMessages.DescriptionRequired);

			RuleFor(model => model.Area)
				.GreaterThan(0)
				.WithMessage(errorMessages.AreaGreaterThan0);

			RuleFor(model => model.PricePerMonth)
				.GreaterThan(0)
				.WithMessage(errorMessages.PricePerMonthGreaterThan0);

			RuleFor(model => model.NrOfRooms)
				.GreaterThan(0)
				.WithMessage(errorMessages.NrOfRoomsGreaterThan0);

			RuleFor(model => model.Latitude)
				.GreaterThanOrEqualTo(-90)
				.LessThanOrEqualTo(90)
				.WithMessage(errorMessages.InvalidLatitude);

			RuleFor(model => model.Longitude)
				.GreaterThanOrEqualTo(-180)
				.LessThanOrEqualTo(180)
				.WithMessage(errorMessages.InvalidLongitude);

			RuleFor(model => model.RealtorId)
				.NotNull()
				.WithMessage(errorMessages.RealtorIdMandatory);
		}
	}
}