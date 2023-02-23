using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feature.ADONET.DAL.Context;
using Feature.ADONET.DAL.DataAccessLayer;
using Feature.ADONET.DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Feature.DAL.Repositories
{
    public static class DALServiceExtentions
    {
        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddTransient<IDALDbContext, DALDbContext>();
            //services.AddSingleton<DALDbContext>();
            services.AddScoped<IDataBaseHandler, DataBaseHandler>();
            services.AddScoped<IDataParameterHandler, DataParameterHandler>();
            services.AddScoped<IDataBaseManager, DataBaseManager>();
            services.AddScoped<IDataAccessLayer, DataAccessLayer>();
            
            //services.AddScoped<IDataAccessLayer, OracleDataAccessLayer>();
        }
    }
}
