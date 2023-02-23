using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Feature.ADONET.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Feature.ADONET.DAL.Context
{
    public class DALDbContext : IDALDbContext
    {
        private readonly IConfiguration _configuration;
        public DALDbContext(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        //public DALDbContext() { }
        public string GetDbConnection()
        {
            var result = _configuration.GetConnectionString("DBConnection");
            return result;
        }
        public string GetProviderName()
        {
            var result = _configuration.GetSection("ProviderName").Value;
            return result;
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DBConnection"));
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}
