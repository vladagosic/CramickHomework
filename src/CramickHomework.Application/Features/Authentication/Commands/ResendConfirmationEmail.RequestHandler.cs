using MediatR;
using CramickHomework.Application.Interfaces;

namespace CramickHomework.Application.Features.Authentication.Commands
{
	public static partial class ResendConfirmationEmail
	{
		public class RequestHandler : IRequestHandler<Request, bool>
		{
			private readonly IEmailService _emailService;

            public RequestHandler(IEmailService emailService)
            {
				_emailService = emailService;
            }

            public async Task<bool> Handle(Request request, CancellationToken cancellationToken)
			{
				return await _emailService.SendConfirmationEmail(request.Email!);
			}
		}
	}
}
