using Feature.Entity.Entities;
using Feature.Repository.Interface.ContextInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Repository.CodeFirst.Context
{
    public class CodeFirstDbContext : DbContext, ICodeFirstDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CodeFirstDbContext(DbContextOptions<CodeFirstDbContext> options, IHttpContextAccessor contextAccessor)
            : base(options)
        {
            _httpContextAccessor = contextAccessor;
        }
        public DbSet<Role> RoleTbl { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var userinfo = (dynamic)_httpContextAccessor.HttpContext.Items["User"];
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "TestUser1";//userinfo.UserId;
                        entry.Entity.CreatedOn = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = "TestUser1";//userinfo.UserId;
                        entry.Entity.LastModifiedOn = DateTime.Now;
                        break;
                }
            }

            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        public int SaveMyChanges()
        {
            var userinfo = (dynamic)_httpContextAccessor.HttpContext.Items["User"];
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "TestUser1";//userinfo.UserId;
                        entry.Entity.CreatedOn = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = "TestUser1";//userinfo.UserId;
                        entry.Entity.LastModifiedOn = DateTime.Now;
                        break;
                }
            }

            int result = base.SaveChanges();
            return result;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}