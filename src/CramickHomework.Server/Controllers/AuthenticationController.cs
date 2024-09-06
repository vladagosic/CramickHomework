using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CramickHomework.Application.Features.Authentication.Commands;

namespace CramickHomework.Server.Controllers
{
	public class AuthenticationController : ApiControllerBase
	{
		[AllowAnonymous]
		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> Login(
			[FromBody] Login.Request request,
			CancellationToken cancellationToken = default)
		{
			var response = await Mediator.Send(request, cancellationToken);

			return Ok(response);
		}

		[AllowAnonymous]
		[HttpPost("register")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<string>> Register(
			[FromBody] Register.Request request,
			CancellationToken cancellationToken = default)
		{
			var response = await Mediator.Send(request, cancellationToken);

			return Ok(response);
		}

		[AllowAnonymous]
		[HttpPost("confirmemail")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<bool>> ConfirmEmail(
			[FromQuery] string email, 
			[FromQuery] string token,
			CancellationToken cancellationToken = default)
		{
			var response = 
				await Mediator.Send(
					new ConfirmEmail.Request 
					{ 
						Email = email, 
						Token = token 
					}, 
					cancellationToken);

			return Ok(response);
		}

		[AllowAnonymous]
		[HttpPost("resendconfirmation")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<bool>> ResendConfirmation(
			[FromQuery] string email,
			CancellationToken cancellationToken = default)
		{
			var response =
				await Mediator.Send(
					new ResendConfirmationEmail.Request
					{
						Email = email
					},
					cancellationToken);

			return Ok(response);
		}

		[AllowAnonymous]
		[HttpPost("resetpassword")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<bool>> ResetPassword(
			[FromBody] ResetPassword.Request request,
			CancellationToken cancellationToken = default)
		{
			var response = await Mediator.Send(request, cancellationToken);

			return Ok(response);
		}

		[AllowAnonymous]
		[HttpPost("sendpasswordreset")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<bool>> SendPasswordReset(
			[FromQuery] string email,
			CancellationToken cancellationToken = default)
		{
			var response =
				await Mediator.Send(
					new SendPasswordResetEmail.Request
					{
						Email = email
					},
					cancellationToken);

			return Ok(response);
		}
	}
}
