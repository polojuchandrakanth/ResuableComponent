using System.Data;
using System.Data.SqlClient;
using Feature.ADONET.DAL.Interfaces;
using Feature.BusinessModel.Common;
using Microsoft.Extensions.Options;

namespace Feature.ADONET.DAL.DataAccessLayer
{
    public class DataBaseManager : IDataBaseManager
    {
        private IDataBaseHandler _dataBaseHandler;
        private IDataAccessLayer _dataAccessLayer;
        private IDataParameterHandler _dataParameterHandler;
        private IDALDbContext _dALDbContext;
        public DataBaseManager(IDataBaseHandler dataBaseHandler, IDataAccessLayer dataAccessLayer,
                               IDALDbContext dALDbContext,IDataParameterHandler dataParameterHandler)
        {
            _dataBaseHandler = dataBaseHandler;
            _dataAccessLayer = dataAccessLayer;
            _dALDbContext = dALDbContext;
            _dataParameterHandler = dataParameterHandler;
        }
        public IDbConnection GetDbConnection()
        {
            return _dataBaseHandler.CreateDatabase();
        }
        public void CloseConnection(IDbConnection dbConnection)
        {
            _dataAccessLayer.Close(dbConnection);
        }
        public IDbDataParameter CreateParameter(string name, object value, DbType dbType)
        {
            string providerName = _dALDbContext.GetProviderName();
            return _dataParameterHandler.CreateParameter(providerName, name, value, dbType, ParameterDirection.Input);
        }
        public IDbDataParameter CreateParameter(string name, int size, object value, DbType dbType)
        {
            string providerName = _dALDbContext.GetProviderName();
            return _dataParameterHandler.CreateParameter(providerName, name, size, value, dbType, ParameterDirection.Input);
        }
        public DataTable GetDataTable(string commandText, CommandType commandType, IDbDataParameter[] parameters)
        {
            using (var connection = GetDbConnection())
            {
                connection.Open();
                using (var command = _dataAccessLayer.CreateCommand(commandText, commandType, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    var dataSet = new DataSet();
                    var dataAdapter = _dataAccessLayer.CreateDataAdapter(command);
                    dataAdapter.Fill(dataSet);
                    return dataSet.Tables[0];
                }
            }
        }
        public IDataReader GetDataReader(string commandText, CommandType commandType, IDbDataParameter[] parameters, IDbConnection connection)
        {
            IDataReader dataReader = null;
            connection = _dataAccessLayer.CreateConnection();
            connection.Open();
            var command = _dataAccessLayer.CreateCommand(commandText, commandType, connection);
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            dataReader = command.ExecuteReader();
            return dataReader;
        }
        public void Delete(string commandText, CommandType commandType, IDbDataParameter[] parameters)
        {
            using (var connection = _dataAccessLayer.CreateConnection())
            {
                connection.Open();
                using (var command = _dataAccessLayer.CreateCommand(commandText, commandType, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();
                }
            }
        }
        public void Insert(string commandText, CommandType commandType, IDbDataParameter[] parameters)
        {
            using (var connection = _dataAccessLayer.CreateConnection())
            {
                connection.Open();
                using (var command = _dataAccessLayer.CreateCommand(commandText, commandType, connection))
                {
                    if (parameters != null && parameters.Any())
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();
                }
            }
        }
        public void Update(string commandText, CommandType commandType, IDbDataParameter[] parameters)
        {
            using (var connection = _dALDbContext.CreateConnection())
            {
                connection.Open();
                using (var command = _dataAccessLayer.CreateCommand(commandText, commandType, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
