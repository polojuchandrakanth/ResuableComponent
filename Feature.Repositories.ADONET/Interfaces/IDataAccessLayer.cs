using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.ADONET.DAL.Interfaces
{
    public interface IDataAccessLayer
    {
        IDbConnection CreateConnection();
        void Open(IDbConnection dbConnection);
        void Close(IDbConnection dbConnection);
        IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection dbConnection);
        IDataAdapter CreateDataAdapter(IDbCommand dbCommand);
        IDbDataParameter CreateParameter(IDbCommand dbCommand);
    }
}
