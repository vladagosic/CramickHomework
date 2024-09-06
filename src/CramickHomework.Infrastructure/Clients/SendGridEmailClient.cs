using Microsoft.Extensions.Configuration;
using CramickHomework.Application;
using CramickHomework.Application.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CramickHomework.Infrastructure.Clients
{
	public class SendGridEmailClient : IEmailClient
	{
		private readonly string _apiKey;

        public SendGridEmailClient(IConfiguration configuration)
        {
            _apiKey = configuration["SendGrid:ApiKey"] ?? throw new ApplicationException("SendGrid API key configuration is missing or not valid in appsettings.");
        }

        public async Task<bool> SendEmailConfirmationEmail(
            string fullName,
            string email,
            string confirmationUrl)
        {
            var client = new SendGridClient(_apiKey);
            var fromEmail = new EmailAddress(Constants.Email.FromEmail, Constants.Email.FromName);
            var subject = "CramickHomework email confirmation";
            var toEmail = new EmailAddress(email, fullName);
            var htmlContent = $"Dear {fullName}, <br><br> Please use this <a href=\"{confirmationUrl}\" target=\"_blank\">LINK</a> to confirm your email address with <strong>CramickHomework</strong><br><br> Thank you!";
            var emailMessage = MailHelper.CreateSingleEmail(fromEmail, toEmail, subject, string.Empty, htmlContent);
            var response = await client.SendEmailAsync(emailMessage);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendPasswordResetEmail(
			string fullName,
			string email,
			string confirmationUrl)
        {
			var client = new SendGridClient(_apiKey);
			var fromEmail = new EmailAddress(Constants.Email.FromEmail, Constants.Email.FromName);
			var subject = "CramickHomework reset password";
			var toEmail = new EmailAddress(email, fullName);
			var htmlContent = $"Dear {fullName}, <br><br> Please use this <a href=\"{confirmationUrl}\" target=\"_blank\">LINK</a> to reset your password with <strong>CramickHomework</strong><br><br> Thank you!";
			var emailMessage = MailHelper.CreateSingleEmail(fromEmail, toEmail, subject, string.Empty, htmlContent);
			var response = await client.SendEmailAsync(emailMessage);

			return response.IsSuccessStatusCode;
		}
    }
}
