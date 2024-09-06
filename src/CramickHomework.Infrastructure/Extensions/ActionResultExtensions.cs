using Microsoft.AspNetCore.Mvc;
using CramickHomework.Application.Data.Responses;

namespace CramickHomework.Infrastructure.Extensions
{
	public static class ActionResultExtensions
	{
		public static ActionResult<TDestination> ToActionResult<TDestination>(this TDestination? model)
			where TDestination : class
		{
			return model == null ?
				(ActionResult<TDestination>)new NotFoundResult()
				: new OkObjectResult(model);
		}

		public static ActionResult<IEnumerable<TDestination>> ToActionResult<TDestination>(this IEnumerable<TDestination>? model)
			where TDestination : class
		{
			return model == null ?
				(ActionResult<IEnumerable<TDestination>>)new NotFoundResult()
				: new OkObjectResult(model);
		}

		public static ActionResult ToActionResult<TResult>(
			this CreateUpdateResult<TResult>? result,
			string actionRoute,
			bool sendResult = false)
			where TResult : class
		{
			return
				result == null ?
					new NotFoundResult()
					: result.IsCreated ?
						new CreatedResult(actionRoute, result.Item)
						: sendResult ?
							new OkObjectResult(result.Item) : new NoContentResult();
		}
	}
}
