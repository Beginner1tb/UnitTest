using WpfNlogSqlTest1.Net5.Interfaces;

namespace WpfNlogSqlTest1.Net5.Services
{
    public class NlogServices
    {
        private readonly INlogRepositories _nlogRepositories;

        public NlogServices(INlogRepositories nlogRepositories)
        {
            _nlogRepositories = nlogRepositories;
        }
        public void LogInfo(string message)
        {
            _nlogRepositories.LogInfo(message);
        }
    }
}