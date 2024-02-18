using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SoftPlc.Exceptions;

public class DbAccessExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not DbAccessException dbAccessException)
            return false;

        var problemDetails =
            new ProblemDetails
            {
                Status = dbAccessException.StatusCode,
                Title = dbAccessException.Title,
                Detail = dbAccessException.Message
            };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}