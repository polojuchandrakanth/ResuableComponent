using Feature.BusinessModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Services.Abstract
{
    public interface IUserMapperService
    {
       
        IEnumerable<UserProfileViewModel> GetAll();
        Task<UserProfileViewModel> GetById(int id);
        void Insert(UserProfileViewModel profile);
        void Update(UserProfileViewModel profile);
        void Delete(object id);
      
    }
}
