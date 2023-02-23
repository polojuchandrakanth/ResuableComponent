using System.Net;
using System.Text.Json;
using Feature.API.Logger;
using Feature.API.Models;

namespace Feature.API.ExceptionMiddleware
{
    public class GlobalExceptionMiddleware
    {
        private RequestDelegate _requestDelegate;
        private ILogger _logger;
        public GlobalExceptionMiddleware(RequestDelegate requestDelegate, ILogger<GlobalExceptionMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }
        public ILogger Get_logger()
        {
            return _logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case ApplicationException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case UnauthorizedAccessException e:
                        // unauthorized error
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case MethodAccessException:
                        // key not found error
                        response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                _logger.LogError(result);
                await response.WriteAsync(result);
            }
        }
    }
}
