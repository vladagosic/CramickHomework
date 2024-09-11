using CramickHomework.Application.Data.Responses;
using CramickHomework.Application.Features.Contacts.Commands;
using CramickHomework.Application.Features.Contacts.Queries;
using CramickHomework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CramickHomework.Server.Controllers
{
	public class ContactsController : ApiControllerBase
	{
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async
			Task<ActionResult<PagedResponse<GetContacts.ContactModel>>> GetAll(
				int? pageSize,
				int? pageNumber,
				string? sort,
				CancellationToken cancellationToken = default)
		{
			var response =
				await Mediator.Send(
					GetContacts.Query.Create(pageSize, pageNumber, sort), cancellationToken);
			return response.ToActionResult();
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<GetContacts.ContactModel>> CreateOrUpdate(
			[FromBody] CreateOrUpdateContact.Request request,
			CancellationToken cancellationToken = default)
		{
			var response = await Mediator.Send(request, cancellationToken);
			return response.ToActionResult($"/api/contacts/{response.Item.Id}", true);
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Delete(
			[FromBody] DeleteContact.Request request,
			CancellationToken cancellationToken = default)
		{
			await Mediator.Send(request, cancellationToken);
			return Ok();
		}
	}
}
