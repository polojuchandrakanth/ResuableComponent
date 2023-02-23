using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feature.Entity.Entities;
using Feature.Repositories.ADONET.Interfaces;

namespace Feature.Repositories.DALRepositories.Interfaces
{
    public interface IDALRepository 
    {
        UserProfile CreateUser(UserProfile userProfile);
    }
}
