using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CramickHomework.Application.Extensions;

namespace CramickHomework.Infrastructure.Mediator
{
	public class PerformanceLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger<PerformanceLoggingBehavior<TRequest, TResponse>> _logger;

		public PerformanceLoggingBehavior(
			IConfiguration configuration,
			ILogger<PerformanceLoggingBehavior<TRequest, TResponse>> logger)
		{
			_configuration = configuration;
			_logger = logger;
		}

		public async Task<TResponse> Handle(TRequest request,
			RequestHandlerDelegate<TResponse> next,
			CancellationToken cancellationToken)
		{
			TResponse response;
			Exception? exception = default;

			var watch = System.Diagnostics.Stopwatch.StartNew();

			try
			{
				response = await next();
			}
			catch (Exception e)
			{
				exception = e;
				throw;
			}
			finally
			{
				watch.Stop();

				if (watch.ElapsedMilliseconds > _configuration.LongRunngingTaskTresholdSeconds() * 1000)
				{
					_logger.LogDebug(GenerateLogMessage(request, exception, watch.ElapsedMilliseconds));
				}
			}

			return response;
		}

		private static string GenerateLogMessage(TRequest request, Exception? exception, long elapsedMilliseconds)
		{
			var message =
				$"Long running request \"{request.GetGenericTypeName(true)}\" execution time {elapsedMilliseconds} ms. Request object:" +
				Environment.NewLine +
				JsonConvert.SerializeObject(request, Formatting.None,
					new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

			if (exception != null)
			{
				message +=
					Environment.NewLine +
					$"{exception.GetGenericTypeName()} occured: {exception.Message}";
			}

			return message;
		}
	}
}
