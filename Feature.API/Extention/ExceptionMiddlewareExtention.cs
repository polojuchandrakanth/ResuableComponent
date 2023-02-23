using Feature.API.ExceptionMiddleware;

namespace Feature.API.Extention
{
    public static class ExceptionMiddlewareExtention
    {
        public static void ConfigureGlobalApplicationMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
