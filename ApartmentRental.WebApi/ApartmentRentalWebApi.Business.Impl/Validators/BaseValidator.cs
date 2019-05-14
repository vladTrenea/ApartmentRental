using System.Linq;
using FluentValidation;
using FluentValidation.Results;

using ValidationException = ApartmentRentalWebApi.Business.Core.Exceptions.ValidationException;

namespace ApartmentRentalWebApi.Business.Impl.Validators
{
	public class BaseValidator<T> : AbstractValidator<T>
	{
		public override ValidationResult Validate(ValidationContext<T> context)
		{
			var result = base.Validate(context);

			if (!result.IsValid)
			{
				throw new ValidationException(result.Errors.FirstOrDefault().ErrorMessage);
			}

			return result;
		}
	}
}