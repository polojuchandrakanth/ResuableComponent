using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Feature.ADONET.DAL.Interfaces
{
    public interface IDALDbContext
    {
        string GetDbConnection();
        string GetProviderName();
        IDbConnection CreateConnection();
        void Open();
        void Close();
    }
}
