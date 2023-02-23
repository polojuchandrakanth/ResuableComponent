using Feature.Entity.Entities;
using Feature.Repository.Dapper.Generic;
using Feature.Repository.Interface.Generic;
using Feature.Repository.Interface.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Repository.Dapper.Repositories
{
    public class UserRepositoryDapper : GenericRepositoryDapper<UserProfile>,IUserRepositoryDapper
    {

        public UserRepositoryDapper(IConfiguration configuration) : base(configuration)
        {

        }
        

     
    }
    }
