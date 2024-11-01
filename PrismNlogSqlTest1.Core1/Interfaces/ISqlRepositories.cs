using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismNlogSqlTest1.Core1.Interfaces
{
    public interface ISqlRepositories
    {
        int GetPriorityNum(string username);
    }
}
