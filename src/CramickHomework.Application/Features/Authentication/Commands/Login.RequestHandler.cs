using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using CramickHomework.Application.Extensions;
using CramickHomework.Application.Features.Users.Domain;
using CramickHomework.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace CramickHomework.Application.Features.Authentication.Commands
{
	public static partial class Login
	{
		public class RequestHandler : IRequestHandler<Request, string>
		{
			private readonly UserManager<ApplicationUser> _userManager;
			private readonly IUserSecurityTokenProvider _userSecurityTokenProvider;
			private readonly IConfiguration _configuration;

			public RequestHandler(
				UserManager<ApplicationUser> userManager,
				IUserSecurityTokenProvider userSecurityTokenProvider,
				IConfiguration configuration)
			{
				_userManager = userManager;
				_userSecurityTokenProvider = userSecurityTokenProvider;
				_configuration = configuration;
			}

			public async Task<string> Handle(Request request, CancellationToken cancellationToken)
			{
				var user = await _userManager.FindByEmailAsync(request.Email!);

				if (user is null 
					|| (_configuration.RequireConfirmedEmail() && !user.EmailConfirmed) 
					|| !await _userManager.CheckPasswordAsync(user, request.Password!))
				{
					throw new ArgumentException($"Unable to authenticate user {request.Email!}");
				}

				return new JwtSecurityTokenHandler().WriteToken(_userSecurityTokenProvider.GetToken(user));
			}
		}
	}
}
