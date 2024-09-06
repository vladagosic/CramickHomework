using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using CramickHomework.Application.Extensions;
using CramickHomework.Application.Features.Users.Domain;
using CramickHomework.Application.Helpers;
using CramickHomework.Application.Interfaces;

namespace CramickHomework.Infrastructure.Services
{
	public class EmailService : IEmailService
	{
		private readonly IEmailClient _emailClient;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IConfiguration _configuration;

		public EmailService(
			IEmailClient emailClient,
			UserManager<ApplicationUser> userManager,
			IConfiguration configuration) 
		{
			_emailClient = emailClient;
			_userManager = userManager;
			_configuration = configuration;
		}

		public async Task<bool> SendConfirmationEmail(string email)
		{
			var user = 
				await _userManager.FindByEmailAsync(email) 
				?? throw new ArgumentException($"Cannot find user for email: {email}");

			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			token = TokenHelper.TokenEncode(token);

			var confirmationUrl = $"{GetClientBaseUrl()}confirm-email?email={email}&token={token}";

			return await _emailClient.SendEmailConfirmationEmail(user.FullName!, user.Email!, confirmationUrl);
		}

		public async Task<bool> SendResetPasswordEmail(string email)
		{
			var user = 
				await _userManager.FindByEmailAsync(email) 
				?? throw new ArgumentException($"Cannot find user for email: {email}");

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			token = TokenHelper.TokenEncode(token);

			var confirmationUrl = $"{GetClientBaseUrl()}reset-password?email={email}&token={token}";

			return await _emailClient.SendPasswordResetEmail(user.FullName!, user.Email!, confirmationUrl);
		}

		private string GetClientBaseUrl()
		{
			var baseUrl = _configuration.ClientApplicationBaseUrl();

			if (string.IsNullOrWhiteSpace(baseUrl))
			{
				throw new ApplicationException("JWT ValidAudience (client application url) is not configured.");
			}

			return 
				baseUrl.EndsWith('/') 
				? baseUrl
				: baseUrl + "/";
		}
	}
}
