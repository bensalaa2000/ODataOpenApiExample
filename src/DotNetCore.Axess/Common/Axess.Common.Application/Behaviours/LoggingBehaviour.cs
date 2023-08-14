﻿using MediatR;
using Microsoft.Extensions.Logging;

namespace Axess.Common.Application.Behaviours;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        _logger.LogInformation($"LoggingBehavior Handling {requestName}");
        TResponse? response = await next();
        _logger.LogInformation($"LoggingBehavior Handled {requestName}");

        return response;
    }
}
