using Feature.JWT.Interface;
using Feature.JWT.Middleware;
using Feature.JWT.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.JWT.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        dynamic settings;
        IConfiguration configuration;
        private IJWTDBContext jWTDBContext;
        private IJWTTokenConfig jWTTokenConfig;
        public JwtMiddleware(RequestDelegate next, IConfiguration iConfig)
        {
            _next = next;
            configuration = iConfig;
            this.settings = configuration.GetSection("appsetting").GetSection("Secret").Value;
            
        }
        public async Task Invoke(HttpContext context, IJWTDBContext _jWTDBContext, IJWTTokenConfig _jWTTokenConfig)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            jWTDBContext = _jWTDBContext;
            jWTTokenConfig = _jWTTokenConfig;
            if (token != null)
                await attachAccountToContext(context, token);

            await _next(context);
        }
        private async Task attachAccountToContext(HttpContext context, string token)
        {
            try
            {
                var accountId = jWTTokenConfig.ValidateJwtToken(token);
                // attach account to context on successful jwt validation
                context.Items["User"] = await jWTTokenConfig.GetUserInformationById(accountId);
            }
            catch
            {
                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
            }
        }
       
    }
}
public static class ValidateJWTTokenMiddlewareExtensions
{
    public static IApplicationBuilder ValidateJWTToken(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtMiddleware>();
    }
}
