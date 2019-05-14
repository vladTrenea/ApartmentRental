using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

using ApartmentRentalWebApi.Business.Core.Services;
using ApartmentRentalWebApi.Business.Core.Settings;
using ApartmentRentalWebApi.Localization.Resources;

namespace ApartmentRentalWebApi.Business.Impl.Services
{
	public class EmailService : IEmailService
	{
		private readonly EmailSettings _emailSettings;

		private readonly ClientAppSettings _clientAppSettings;

		private readonly ILabelMessages _labelMessages;

		public EmailService(EmailSettings emailSettings, ClientAppSettings clientAppSettings, ILabelMessages labelMessages)
		{
			_emailSettings = emailSettings;
			_clientAppSettings = clientAppSettings;
			_labelMessages = labelMessages;
		}

		public async Task SendAccountConfirmationEmail(string email, string token)
		{
			var subject = _labelMessages.AccountConfirmationEmailSubject;
			var body = string.Format(_labelMessages.AccountConfirmationEmailBody, email, $"{_clientAppSettings.BaseUrl}{_clientAppSettings.AccountConfirmationPath}/{token}");

			await SendEmail(email, subject, body);
		}

		public async Task SendAccountCreationEmail(string email, string token)
		{
			var subject = _labelMessages.AccountCreationEmailSubject;
			var body = string.Format(_labelMessages.AccountCreationEmailBody, email, $"{_clientAppSettings.BaseUrl}{_clientAppSettings.ResetPasswordPath}/{token}");

			await SendEmail(email, subject, body);
		}

		public async Task SendPasswordResetEmail(string email, string token)
		{
			var subject = _labelMessages.PasswordResetEmailSubject;
			var body = string.Format(_labelMessages.PasswordResetEmailBody, email, $"{_clientAppSettings.BaseUrl}{_clientAppSettings.ResetPasswordPath}/{token}");

			await SendEmail(email, subject, body);
		}

		private async Task SendEmail(string email, string subject, string body)
		{
			using (var client = new SmtpClient())
			{
				var credential = new NetworkCredential
				{
					UserName = _emailSettings.Email,
					Password = _emailSettings.Password
				};

				client.Credentials = credential;
				client.Host = _emailSettings.Host;
				client.Port = int.Parse(_emailSettings.Port);
				client.EnableSsl = true;

				using (var emailMessage = new MailMessage())
				{
					emailMessage.To.Add(new MailAddress(email));
					emailMessage.From = new MailAddress(_emailSettings.Email);
					emailMessage.Subject = subject;
					emailMessage.Body = body;
					emailMessage.IsBodyHtml = true;
					client.Send(emailMessage);
				}
			}

			await Task.CompletedTask;
		}
	}
}