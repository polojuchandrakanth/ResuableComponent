using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace Feature.API.SeriLogMiddleware
{
    public class SerilogMiddleware
    {
        public RequestDelegate _requestDelegate;
        private readonly ILogger<SerilogMiddleware> _logger;
        public SerilogMiddleware(RequestDelegate requestDelegate, ILogger<SerilogMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        private Task HandleException(HttpContext context, Exception ex)
        {
            _logger.LogError(ex.ToString());
            var errorMessageObject = new { Message = ex.Message, Code = "system_error" };

            var errorMessage = JsonConvert.SerializeObject(errorMessageObject);
            if (errorMessage.Contains("400"))
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            if(errorMessage.Contains("401"))
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            if (errorMessage.Contains("404"))
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            if (errorMessage.Contains("405"))
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
            }
            if (errorMessage.Contains("500"))
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            _logger.LogError(errorMessage);
            return context.Response.WriteAsync(errorMessage);
        }
    }
}
