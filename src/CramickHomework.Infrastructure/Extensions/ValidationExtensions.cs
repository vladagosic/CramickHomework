using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CramickHomework.Infrastructure.Extensions
{
	public static class ValidationExtensions
	{
		public static BadRequestObjectResult CreateValidationProblemDetailsResponse(this HttpContext context, ModelStateDictionary modelState)
		{
			ValidationProblemDetails problemDetails = CreateValidationProblemDetails(context, modelState);
			return ToBadRequestObjectResult(problemDetails);
		}

		public static BadRequestObjectResult CreateValidationProblemDetailsResponse(this HttpContext context, ValidationException validationException)
		{
			ValidationProblemDetails problemDetails = CreateValidationProblemDetails(context);
			CopyErrorsFromValidationException(problemDetails, validationException.Errors);
			return ToBadRequestObjectResult(problemDetails);
		}

		public static BadRequestObjectResult CreateValidationProblemDetailsResponse(this HttpContext context, BadHttpRequestException badHttpRequestException)
		{
			ValidationProblemDetails problemDetails = CreateValidationProblemDetails(context);
			problemDetails.Detail = badHttpRequestException.Message;
			return ToBadRequestObjectResult(problemDetails);
		}

		public static BadRequestObjectResult CreateValidationProblemDetailsResponse(this HttpContext context, ArgumentException argumentException)
		{
			ValidationProblemDetails problemDetails = CreateValidationProblemDetails(context);
			problemDetails.Detail = argumentException.Message;
			return ToBadRequestObjectResult(problemDetails);
		}

		private static ValidationProblemDetails CreateValidationProblemDetails(HttpContext context,
			ModelStateDictionary? modelState = null)
		{
			ValidationProblemDetails details = modelState != null ?
				new ValidationProblemDetails(modelState) :
				new ValidationProblemDetails
				{
					Instance = context.Request.Path,
					Status = StatusCodes.Status400BadRequest,
					Type = "https://asp.net/core",
					Detail = "Please refer to the errors property for additional details."
				};
			return details;
		}

		private static BadRequestObjectResult ToBadRequestObjectResult(ValidationProblemDetails problemDetails)
		{
			return new BadRequestObjectResult(problemDetails)
			{
				ContentTypes = { "application/problem+json", "application/problem+xml" }
			};
		}

		private static void CopyErrorsFromValidationException(ValidationProblemDetails problemDetails, IEnumerable<ValidationFailure> validationExceptionErrors)
		{
			foreach (ValidationFailure validationExceptionError in validationExceptionErrors)
			{
				string key = validationExceptionError.PropertyName;
				if (!problemDetails.Errors.TryGetValue(key, out string[]? messages))
				{
					messages = [];
				}

				messages = [.. messages, .. new[] { validationExceptionError.ErrorMessage }];
				problemDetails.Errors[key] = messages;
			}
		}
	}
}
