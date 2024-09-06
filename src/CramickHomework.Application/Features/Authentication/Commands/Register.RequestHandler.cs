using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using CramickHomework.Application.Extensions;
using CramickHomework.Application.Features.Users.Domain;
using CramickHomework.Application.Interfaces;

namespace CramickHomework.Application.Features.Authentication.Commands
{
	public static partial class Register
	{
		public class RequestHandler : IRequestHandler<Request, string?>
		{
			private readonly UserManager<ApplicationUser> _userManager;
			private readonly IConfiguration _configuration;
			private readonly IMediator _mediator;
			private readonly IEmailService _emailService;

            public RequestHandler(
				UserManager<ApplicationUser> userManager,
				IConfiguration configuration,
				IMediator mediator,
				IEmailService emailService)
            {
				_userManager = userManager;
                _configuration = configuration;
				_mediator = mediator;
				_emailService = emailService;
            }

            public async Task<string?> Handle(Request request, CancellationToken cancellationToken)
			{
				var userByEmail = await _userManager.FindByEmailAsync(request.Email!);

                if (userByEmail is not null)
                {
					throw new ArgumentException($"User with email {request.Email} already exists.");
                }

				ApplicationUser user = new()
				{
					Email = request.Email,
					FullName = request.FullName
				};

				var result = await _userManager.CreateAsync(user, request.Password!);

				if (!result.Succeeded)
				{
					throw new ArgumentException($"Unable to register user {request.Email} errors: {result.GetErrorsText()}");
				}

				if (!await _emailService.SendConfirmationEmail(request.Email!))
				{
					throw new ArgumentException($"Could not send confirmation email to {request.Email}");
				}

				return
					_configuration.RequireConfirmedEmail()
					? null
					: await _mediator.Send(
						new Login.Request()
						{
							Email = request.Email,
							Password = request.Password
						},
						cancellationToken);
			}
		}
	}
}
