using WpfNlogSqlTest1.Net5.Interfaces;

namespace WpfNlogSqlTest1.Net5.Services
{
    public class SqlServices
    {
        private readonly ISqlRepositories _sqlRepositories;

        public SqlServices(ISqlRepositories sqlRepositories)
        {
            _sqlRepositories = sqlRepositories;
        }
        
        public int GetPriorityNum(string username)
        {
            return _sqlRepositories.GetPriorityNum(username);
        }
    }
}