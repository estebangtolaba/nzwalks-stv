using System.Net;

namespace NZWalks.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> Logger;
        private readonly RequestDelegate Next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,
            RequestDelegate next)
        {
            Logger = logger;
            Next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await Next(httpContext);
            }
            catch (Exception ex) 
            {
                var errorId = Guid.NewGuid();

                Logger.LogError(ex, $"{errorId} : {ex.Message}");
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong bro."
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
