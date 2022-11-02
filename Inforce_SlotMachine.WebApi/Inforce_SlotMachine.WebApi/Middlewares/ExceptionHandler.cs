using Inforce_SlotMachine.Common.Exceptions;
using System.Net;
using System.Text;

namespace Inforce_SlotMachine.WebApi.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionHandler(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch(Exception ex)
            {
                var status = ex switch
                {
                    NotFoundException => HttpStatusCode.NotFound,
                    ValidationException => HttpStatusCode.BadRequest,
                    _ => HttpStatusCode.InternalServerError,
                };

                await HandleException(context, ex, status);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex, HttpStatusCode statusCode)
        {
            var errorMessage = BuildErrorMessage(ex);

            _logger.LogError("Status: {statusCode}\tError: {errorMessage}", statusCode, errorMessage);

            var errorBody = new { Error = ex.Message };
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            //await context.Response.WriteAsync(JsonConvert.SerializeObject(errorBody)); //TODO: Add NewtonsJson.Bson nuGet
            await context.Response.WriteAsync(ex.Message);
        }

        private static string BuildErrorMessage(Exception? ex)
        {
            var sb = new StringBuilder();

            while(ex != null)
            {
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine(ex.Source);

                ex = ex.InnerException;
            }

            var errorMessage = sb.ToString();

            if (string.IsNullOrEmpty(errorMessage))
                errorMessage = "Unknown error occured";

            return errorMessage;
        }
    }
}
