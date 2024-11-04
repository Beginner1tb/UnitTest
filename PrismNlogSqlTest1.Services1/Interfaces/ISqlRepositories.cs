using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismNlogSqlTest1.Services1.Interfaces
{
    public interface ISqlRepositories
    {
        int GetPriorityNum(string username);
    }
}
