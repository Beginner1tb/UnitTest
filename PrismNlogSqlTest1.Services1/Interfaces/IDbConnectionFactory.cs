using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismNlogSqlTest1.Services1.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
        IDbDataParameter CreateParameter(string name, object value);
    }
}
