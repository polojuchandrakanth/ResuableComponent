using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.ADONET.DAL.Interfaces
{
    public interface IDataParameterHandler
    {
        IDbDataParameter CreateParameter(string providerName, string name, object value, DbType dbType, ParameterDirection direction = ParameterDirection.Input);
        IDbDataParameter CreateParameter(string providerName, string name, int size, object value, DbType dbType, ParameterDirection direction = ParameterDirection.Input);
        IDbDataParameter CreateSqlParameter(string name, object value, DbType dbType, ParameterDirection direction = ParameterDirection.Input);
        IDbDataParameter CreateSqlParameter(string name, int size, object value, DbType dbType, ParameterDirection direction);
        IDbDataParameter CreateOracleParameter(string name, object value, DbType dbType, ParameterDirection direction);
        IDbDataParameter CreateOracleParameter(string name, int size, object value, DbType dbType, ParameterDirection direction);
    }
}
