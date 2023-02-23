using Feature.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Services.Abstract
{
    public interface IRoleService
    {

        IEnumerable<Role> GetAll();
        Task<Role> GetById(int id);
        void Insert(Role profile);
        void Update(Role profile);
        void Delete(object id);

    }
}