using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feature.Entity.Entities;

namespace Feature.Services.Abstract
{
    public interface IDALService
    {
        void CreateUser(UserProfile userProfile);
        IEnumerable<UserProfile> GetUserProfiles();
        IEnumerable GetUserById(string userId);
        void UpdateUserProfile(UserProfile userProfile);
        void DeleteUserProfile(string userId);
    }
}
