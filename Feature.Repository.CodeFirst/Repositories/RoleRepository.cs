using Feature.Entity.Entities;
using Feature.Repository.CodeFirst.Context;
using Feature.Repository.CodeFirst.Generic;
using Feature.Repository.Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Repository.CodeFirst.Repositories
{
    public class RoleRepository : GenericCodeFirstRepository<Role>, IRoleRepository
    {
        private CodeFirstDbContext _context;


        public RoleRepository(CodeFirstDbContext context) : base(context)
        {

        }



    }
}