using MediatR;
using Microsoft.Extensions.Logging;
using CramickHomework.Infrastructure.Logging;
using Serilog.Context;

namespace CramickHomework.Infrastructure.Mediator
{
	public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

		public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
		{
			_logger = logger;
		}

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			var requestType = typeof(TRequest).FullName;
			using (LogContext.PushProperty("MediatRRequestType", requestType))
			{
				_logger.LogInformation("Handling {RequestType}", requestType);

				if (_logger.IsEnabled(LogLevel.Debug) && RequestLogging.ShouldRequestTypeBeLogged(typeof(TRequest)))
				{
					_logger.LogDebug("{RequestType} Request Payload: {Payload}", requestType, RequestLogging.SerializeRequest(request));
				}

				TResponse response = await next();
				_logger.LogInformation("Handled {RequestType}", requestType);
				return response;
			}
		}
	}
}
