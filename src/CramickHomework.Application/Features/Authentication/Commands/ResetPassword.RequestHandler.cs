using MediatR;
using Microsoft.AspNetCore.Identity;
using CramickHomework.Application.Extensions;
using CramickHomework.Application.Features.Users.Domain;
using CramickHomework.Application.Helpers;

namespace CramickHomework.Application.Features.Authentication.Commands
{
	public static partial class ResetPassword
	{
		public class RequestHandler : IRequestHandler<Request, bool>
		{
			private readonly UserManager<ApplicationUser> _userManager;

			public RequestHandler(UserManager<ApplicationUser> userManager)
			{
				_userManager = userManager;
			}

			public async Task<bool> Handle(Request request, CancellationToken cancellationToken)
			{
				var user = 
					await _userManager.FindByEmailAsync(request.Email!) 
					?? throw new ArgumentException($"Cannot find user for email: {request.Email}");

				var token = TokenHelper.TokenDecode(request.Token!);

				var result = await _userManager.ResetPasswordAsync(user, token, request.Password!);

				if (!result.Succeeded)
				{
					throw new ArgumentException($"Unable to register user {request.Email} errors: {result.GetErrorsText()}");
				}

				return result.Succeeded;
			}
		}
	}
}
