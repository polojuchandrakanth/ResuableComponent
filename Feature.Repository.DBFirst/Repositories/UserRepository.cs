using Feature.BusinessModel.Common;
using Feature.BusinessModel.ViewModel;
using Feature.Entity.Entities;
using Feature.JWT;
using Feature.JWT.Interface;
using Feature.Repository.DBFirst.Context;
using Feature.Repository.DBFirst.Generic;
using Feature.Repository.Interface.Generic;
using Feature.Repository.Interface.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Repository.DBFirst.Repositories
{
    public class UserRepository : GenericRepository<UserProfile>, IUserRepository
    {
        private ApplicationDbContext _context;
        IOptions<Appsetting> _settings;
        private IJWTTokenConfig iJWTConfig;
        public UserRepository(ApplicationDbContext context, IOptions<Appsetting> settings,
                              IJWTTokenConfig _iJWTConfig) :base(context) 
        {
            _context = context;
            _settings = settings;
            iJWTConfig = _iJWTConfig;
        }

        public async Task<LoginResponse> Login(UserLogins userLogins)
        {
            try
            {
                var user = _context.UserProfileTbl.Where(x => (x.UserId.Equals(userLogins.UserName)) && (x.Password.Equals(userLogins.Password))).FirstOrDefault();
                //var user = _context.UserProfileTbl.FirstOrDefault();
                if (user != null)
                {
                    var token = iJWTConfig.GenerateJwtToken(user.UserId);
                    if (token != null)
                    {
                        user.RefreshToken = iJWTConfig.GenerateRefreshToken();

                        //ToDO update refreshtoken in database for user record
                        await _context.SaveChangesAsync();
                        LoginResponse loginResponse = new LoginResponse()
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Token = token,
                            RefreshToken = user.RefreshToken,
                        };
                        return loginResponse;
                    }
                }
                
                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
