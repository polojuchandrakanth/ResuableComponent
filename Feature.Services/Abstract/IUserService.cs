using Feature.BusinessModel.ViewModel;
using Feature.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Services.Abstract
{
    public interface IUserService
    {
        Task<LoginResponse> Login(UserLogins userLogins);
        IEnumerable<UserProfile> GetAll();
        Task<UserProfile> GetById(int id);
        void Insert(UserProfile profile);
        void Update(UserProfile profile);
        void Delete(object id);
        Task<UserProfile> Filter(Expression<Func<UserProfile, bool>> Predicate);
    }
}
