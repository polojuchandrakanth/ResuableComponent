using Dapper.Contrib.Extensions;
using Feature.Repository.Interface.Generic;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Repository.Dapper.Generic
{
    public class GenericRepositoryDapper<T> : IGenericRepositoryDapper<T> where T : class
    {
        private readonly IConfiguration configuration;
        protected IDbConnection DbConnection { get; private set; }

        public GenericRepositoryDapper(IConfiguration configuration)
        {
            this.configuration = configuration;
            DbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
        }
        public IEnumerable<T> GetAll()
        {
            DbConnection.Open();
            var results = DbConnection.GetAll<T>();
            return results.AsQueryable();
        }
        public T GetById(object id)
        {
            DbConnection.Open();
            return DbConnection.Get<T>(id);
        }

        public void Insert(T obj)
        {
            DbConnection.Open();
            var inserted = DbConnection.Insert<T>(obj);
            DbConnection.Close();
        }
        public void Update(T obj)
        {
            DbConnection.Open();
            var result = DbConnection.Update<T>(obj);
            DbConnection.Close();

        }
        public void Delete(object id)
        {
            DbConnection.Open();
            var entity = DbConnection.Get<T>(id);
            var res = DbConnection.Delete<T>(entity);
        }
    }
    }
