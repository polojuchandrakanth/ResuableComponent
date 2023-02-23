using Feature.BusinessModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Services.Abstract
{
    public interface IRoleMapperService
    {
        IEnumerable<RoleModel> GetAll();
        Task<RoleModel> GetById(int id);
        void Insert(RoleModel profile);
        void Update(RoleModel profile);
        void Delete(object id);
    }
}
