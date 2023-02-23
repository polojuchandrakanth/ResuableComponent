using Feature.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Services.Abstract
{
    public interface IUserServiceDapper
    {
        IEnumerable<UserProfile> GetAll();
        UserProfile GetById(object id);
        void Insert(UserProfile obj);
        void Update(UserProfile obj);
        void Delete(object id);
    }
}
