using System.Data;

namespace Feature.ADONET.DAL.Interfaces
{
    public interface IDataBaseHandler
    {
        IDbConnection CreateDatabase();
    }
}
