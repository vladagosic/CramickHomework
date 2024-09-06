using Microsoft.AspNetCore.Mvc;
using CramickHomework.Application.Data.Responses;
using CramickHomework.Application.Features.Users.Commands;
using CramickHomework.Application.Features.Users.Queries;
using CramickHomework.Infrastructure.Extensions;

namespace CramickHomework.Server.Controllers
{
	public class UsersController : ApiControllerBase
	{

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async
			Task<ActionResult<PagedResponse<GetUsers.UserModel>>> GetAll(
				int? pageSize,
				int? pageNumber,
				string? sort,
				CancellationToken cancellationToken = default)
		{
			var response =
				await Mediator.Send(
					GetUsers.Query.Create(pageSize, pageNumber, sort), cancellationToken);
			return response.ToActionResult();
		}

		[HttpGet("profile")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async
			Task<ActionResult<GetCurrentUser.UserModel>> GetCurrentUser(
				CancellationToken cancellationToken = default)
		{
			var response =
				await Mediator.Send(new GetCurrentUser.Query(), cancellationToken);
			return response.ToActionResult();
		}


		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<GetUsers.UserModel>> CreateUser(
			[FromBody] CreateUser.Request request,
			CancellationToken cancellationToken = default)
		{
			var response = await Mediator.Send(request, cancellationToken);
			return response.ToActionResult($"/api/users/{response.Item.Id}", true);
		}
	}
}
