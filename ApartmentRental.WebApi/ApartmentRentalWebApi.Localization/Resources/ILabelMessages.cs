namespace ApartmentRentalWebApi.Localization.Resources
{
	public interface ILabelMessages
	{
		string AccountConfirmationEmailBody { get; }
		string AccountConfirmationEmailSubject { get; }
		string AccountCreationEmailSubject { get; }
		string AccountCreationEmailBody { get; }
		string PasswordResetEmailSubject { get; }
		string PasswordResetEmailBody { get; }
	}
}