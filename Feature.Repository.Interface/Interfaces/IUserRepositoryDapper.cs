using Feature.Entity.Entities;
using Feature.Repository.Interface.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Repository.Interface.Interfaces
{
    public interface IUserRepositoryDapper : IGenericRepositoryDapper<UserProfile> { }
    
}
