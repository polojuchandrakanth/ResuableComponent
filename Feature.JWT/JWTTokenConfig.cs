using Feature.BusinessModel.Common;
using Feature.JWT.Context;
using Feature.JWT.Interface;
using Feature.JWT.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Feature.JWT
{
    public class JWTTokenConfig: IJWTTokenConfig
    {
        private dynamic settings;
        private IJWTDBContext jWTDBContext;
        public JWTTokenConfig(IOptions<Appsetting> _settings, IJWTDBContext _jWTDBContext)
        {
            this.settings = _settings.Value;
            jWTDBContext = _jWTDBContext;
        }
        public string GenerateJwtToken(string accountId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(this.settings.Secret); 
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", accountId.ToString()) }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {

                throw;
            }
            //return null;
        }
        public string? ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.settings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                //var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                var accountId = jwtToken.Claims.First(x => x.Type == "id").Value;

                // return account id from JWT token if validation successful
                return accountId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public async Task<UserDetails> GetUserInformationById(string id)
        {
            UserDetails user = new UserDetails();
            try
            {
                var userdetails = jWTDBContext.UserDetailsTbl.Where(x => x.UserId.Equals(id)).FirstOrDefault();
                user = (UserDetails)userdetails;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return user;
        }
    }
}
