﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CramickHomework.Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public abstract class ApiControllerBase : ControllerBase
	{
		private IMediator? _mediator;
		protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
	}
}
