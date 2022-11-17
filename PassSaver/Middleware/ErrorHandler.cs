using Microsoft.AspNetCore.Http;
using PassSaver.Exceptions;

namespace PassSaver.Middleware
{
    public class ErrorHandler : IMiddleware
    {
        private readonly ILogger<ErrorHandler> _logger;
        public ErrorHandler(ILogger<ErrorHandler> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
               await next.Invoke(context);
            }
            catch (UserNotFoundException notFoundException) 
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (UserCredentialsTakenException userCredentialsTakenException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(userCredentialsTakenException.Message);
            }
            catch (Exception e) 
            {
                _logger.LogError(e,e.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
