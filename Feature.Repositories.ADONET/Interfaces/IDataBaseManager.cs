using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.ADONET.DAL.Interfaces
{
    public interface IDataBaseManager
    {
        IDbConnection GetDbConnection();
        void CloseConnection(IDbConnection dbConnection);
        IDbDataParameter CreateParameter(string name, object value, DbType dbType);
        IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType);
        DataTable GetDataTable(string commandText, CommandType commandType, IDbDataParameter[] parameters);
        IDataReader GetDataReader(string commandText, CommandType commandType, IDbDataParameter[] parameters, IDbConnection connection);
        void Delete(string commandText, CommandType commandType, IDbDataParameter[] parameters);
        void Insert(string commandText, CommandType commandType, IDbDataParameter[] parameters);
        void Update(string commandText, CommandType commandType, IDbDataParameter[] parameters);
    }
}
