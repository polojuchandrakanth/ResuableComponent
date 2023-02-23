using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feature.ADONET.DAL.Interfaces;
using Feature.BusinessModel.Common;
using Microsoft.Extensions.Options;

namespace Feature.ADONET.DAL.DataAccessLayer
{
    public class DataParameterHandler : IDataParameterHandler
    {   
        public IDbDataParameter CreateParameter(string providerName, string name, object value, DbType dbType, ParameterDirection direction = ParameterDirection.Input)
        {
            IDbDataParameter parameter = null;
            switch (providerName.ToLower())
            {
                case "system.data.sqlclient":
                    var sqlParameter = CreateSqlParameter(name, value, dbType, direction);
                    return sqlParameter;
                    break;
                case "system.data.oracleclient":
                    return CreateOracleParameter(name, value, dbType, direction);
                    break;
            }
            return parameter;
        }
        public IDbDataParameter CreateParameter(string providerName, string name, int size, object value, DbType dbType, ParameterDirection direction = ParameterDirection.Input)
        {
            IDbDataParameter parameter = null;
            switch (providerName.ToLower())
            {
                case "system.data.sqlclient":
                    var sqlParameter = CreateSqlParameter(name, size, value, dbType, direction);
                    return sqlParameter;
                case "system.data.oracleclient":
                    return CreateOracleParameter(name, size, value, dbType, direction);
            }
            return parameter;
        }
        public IDbDataParameter CreateSqlParameter(string name, object value, DbType dbType, ParameterDirection direction = ParameterDirection.Input)
        {
            IDbDataParameter parameter =  new SqlParameter
            {
                DbType = dbType,
                ParameterName = name,
                Direction = direction,
                Value = value
            };
            
            return parameter;
        }
        public IDbDataParameter CreateSqlParameter(string name, int size, object value, DbType dbType, ParameterDirection direction)
        {
            return new SqlParameter
            {
                DbType = dbType,
                Size = size,
                ParameterName = name,
                Direction = direction,
                Value = value
            };
        }
        public IDbDataParameter CreateOracleParameter(string name, object value, DbType dbType, ParameterDirection direction)
        {
            return new OracleParameter
            {
                DbType = dbType,
                ParameterName = name,
                Direction = direction,
                Value = value
            };
        }

        public IDbDataParameter CreateOracleParameter(string name, int size, object value, DbType dbType, ParameterDirection direction)
        {
            return new OracleParameter
            {
                DbType = dbType,
                Size = size,
                ParameterName = name,
                Direction = direction,
                Value = value
            };
        }
    }
}
