using EGAR.SharedKernel.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EGAR.Application.Behaviors;

public class ResultPipelineBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TResponse : class
{
    readonly ILogger<ResultPipelineBehavior<TRequest, TResponse>> _logger;

    public ResultPipelineBehavior(ILogger<ResultPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            var response = await next();

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling {Request}", typeof(TRequest).Name);

            var resultType = typeof(TResponse);
            if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Result<>))
            {
                var errorResult = Activator.CreateInstance(
                    resultType,
                    false,
                    default,
                    ex.Message
                );
                return (TResponse)errorResult;
            }

            throw; 
        }
    }
}

