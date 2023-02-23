using Feature.JWT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.JWT.Interface
{
    public interface IJWTTokenConfig
    {
        public string GenerateJwtToken(string accountId);
        public string? ValidateJwtToken(string token);
        public string GenerateRefreshToken();
        Task<UserDetails> GetUserInformationById(string id);
    }
}
