using System.Data;
using Feature.ADONET.DAL.Interfaces;

namespace Feature.ADONET.DAL.DataAccessLayer
{
    public class DataBaseHandler : IDataBaseHandler
    {
        private readonly IDataAccessLayer _dataAccessLayer;
        public DataBaseHandler(IDataAccessLayer dataAccessLayer) 
        {
            _dataAccessLayer = dataAccessLayer;
        }

        public IDbConnection CreateDatabase()
        {
            return _dataAccessLayer.CreateConnection();
        }
    }
}
