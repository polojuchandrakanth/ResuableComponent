using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feature.ADONET.DAL.Context;
using Feature.ADONET.DAL.Interfaces;

namespace Feature.ADONET.DAL.DataAccessLayer
{
    public class DataAccessLayer : IDataAccessLayer
    {
        private IDALDbContext _dALDbContext;

        public DataAccessLayer(IDALDbContext dALDbContext)
        {
            _dALDbContext = dALDbContext;
        }

        public IDbConnection CreateConnection()
        {
            IDbConnection connection = null;
            string connectionString = _dALDbContext.GetDbConnection();
            string providerName = _dALDbContext.GetProviderName();
            switch (providerName.ToLower())
            {
                case "system.data.sqlclient":
                    connection = new SqlConnection(connectionString);
                    break;
                case "system.data.oracleclient":
                    connection = new OdbcConnection(connectionString);
                    break;
            }
            return connection;
        }
        public void Open(IDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }
        public void Close(IDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public IDbCommand CreateCommand(string commandText, CommandType commandType, IDbConnection dbConnection)
        {
            string providerName = _dALDbContext.GetProviderName();
            switch (providerName.ToLower())
            {
                case "system.data.sqlclient":
                    return new SqlCommand
                    {
                        CommandText = commandText,
                        CommandType = commandType,
                        Connection = (SqlConnection)dbConnection
                    };
                    break;
                case "system.data.oracleclient":
                    return new OdbcCommand
                    {
                        CommandText = commandText,
                        CommandType = commandType,
                        Connection = (OdbcConnection)dbConnection
                    };
                    break;
            }
            return null;
        }

        public IDataAdapter CreateDataAdapter(IDbCommand dbCommand)
        {
            string providerName = _dALDbContext.GetProviderName();
            switch (providerName.ToLower())
            {
                case "system.data.sqlclient":
                    var sqlAdapter =  new SqlDataAdapter((SqlCommand)dbCommand);
                    return sqlAdapter;
                    break;
                case "system.data.oracleclient":
                    var odbcAdapter = new OdbcDataAdapter((OdbcCommand)dbCommand);
                    return odbcAdapter;
                    break;
            }
            return null;
        }

        public IDbDataParameter CreateParameter(IDbCommand dbCommand)
        {
            string providerName = _dALDbContext.GetProviderName();
            switch (providerName.ToLower())
            {
                case "system.data.sqlclient":
                    SqlCommand sqlCommand = (SqlCommand)dbCommand;
                    return sqlCommand.CreateParameter();
                    break;
                case "system.data.odbcclient":
                    OdbcCommand odbcCommand = (OdbcCommand)dbCommand;
                    return odbcCommand.CreateParameter();
                    break ;
            }
            return null;
        }
    }
}
