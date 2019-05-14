using Microsoft.Extensions.Localization;

namespace ApartmentRentalWebApi.Localization.Resources
{
	public class BaseResourceMessages<T>
	{
		private readonly IStringLocalizer<T> _localizer;

		public BaseResourceMessages(IStringLocalizer<T> localizer)
		{
			_localizer = localizer;
		}

		protected string GetString(string name)
		{
			var val = _localizer[name];

			return val;
		}
	}
}