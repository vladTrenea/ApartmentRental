using Microsoft.Extensions.Localization;

namespace ApartmentRentalWebApi.Localization.Resources
{
	public class LabelMessages : BaseResourceMessages<LabelMessages>, ILabelMessages
	{
		public LabelMessages(IStringLocalizer<LabelMessages> localizer) : base(localizer)
		{

		}

		public string AccountConfirmationEmailBody => GetString(nameof(AccountConfirmationEmailBody));
		public string AccountConfirmationEmailSubject => GetString(nameof(AccountConfirmationEmailSubject));
		public string AccountCreationEmailSubject => GetString(nameof(AccountCreationEmailSubject));
		public string AccountCreationEmailBody => GetString(nameof(AccountConfirmationEmailBody));
		public string PasswordResetEmailSubject => GetString(nameof(PasswordResetEmailSubject));
		public string PasswordResetEmailBody => GetString(nameof(PasswordResetEmailBody));
	}
}