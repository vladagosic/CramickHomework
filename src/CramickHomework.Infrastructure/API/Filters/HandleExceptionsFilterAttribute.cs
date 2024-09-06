using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CramickHomework.Application.Extensions;
using CramickHomework.Infrastructure.Extensions;
using System.Diagnostics;
using System.Net;

namespace CramickHomework.Infrastructure.API.Filters
{
	public class HandleExceptionsFilterAttribute : ExceptionFilterAttribute
	{
		private readonly bool _useDeveloperExceptions;
		private readonly bool _hideSystemExceptionMessages;
		public HandleExceptionsFilterAttribute(IConfiguration configuration)
		{
			_useDeveloperExceptions = configuration.UseDeveloperExceptions();
			_hideSystemExceptionMessages = configuration.HideSystemExceptionMessages();
		}

		public override void OnException(ExceptionContext context)
		{
			ILogger<HandleExceptionsFilterAttribute> logger = context.HttpContext.Resolve<ILogger<HandleExceptionsFilterAttribute>>();

			if (context.Exception is ValidationException validationException)
			{
				logger.LogDebug(context.Exception, "Validation exception");
				context.Result = context.HttpContext.CreateValidationProblemDetailsResponse(validationException);
				return;
			}

			if (context.Exception is BadHttpRequestException badRequestException)
			{
				logger.LogDebug(context.Exception, "Bad Request exception");
				context.Result = context.HttpContext.CreateValidationProblemDetailsResponse(badRequestException);
				return;
			}

			if (context.Exception is ArgumentException argumentException)
			{
				logger.LogDebug(context.Exception, "Bad Request exception");
				context.Result = context.HttpContext.CreateValidationProblemDetailsResponse(argumentException);
				return;
			}

			logger.LogError(context.Exception, "Unexpected error");

			var accept = context.HttpContext.Request.GetTypedHeaders().Accept;
			if (accept != null && accept.All(header => header.MediaType != "application/json"))
			{
				// server does not accept Json, leaving to default MVC error page handler.
				return;
			}

			var jsonResult =
				new JsonResult(
					GetProblemDetails(context))
				{
					StatusCode = (int)HttpStatusCode.InternalServerError,
					ContentType = "application/problem+json"
				};
			context.Result = jsonResult;
		}

		private ProblemDetails GetProblemDetails(ExceptionContext context)
		{
			string errorDetail = _useDeveloperExceptions
				? context.Exception.Demystify().ToString()
				: _hideSystemExceptionMessages 
					? "The instance value should be used to identify the problem when calling customer support"
					: context.Exception.Message;

			var problemDetails =
				new ProblemDetails
				{
					Title = "An unexpected error occurred!",
					Instance = context.HttpContext.Request.Path,
					Status = StatusCodes.Status500InternalServerError,
					Detail = errorDetail
				};

			return problemDetails;
		}
	}
}
