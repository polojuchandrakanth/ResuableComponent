using Feature.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Repository.Interface.ContextInterface
{
    public interface ICodeFirstDbContext
    {
        DbSet<Role> RoleTbl { get; set; }
        int SaveMyChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}