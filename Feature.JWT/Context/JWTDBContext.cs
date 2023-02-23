using Feature.JWT.Interface;
using Feature.JWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Feature.JWT.Context
{
    public class JWTDBContext: DbContext, IJWTDBContext
    {
        public JWTDBContext(DbContextOptions<JWTDBContext> options)
            : base(options)
        {
            
        }
        public  DbSet<UserDetails> UserDetailsTbl { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
