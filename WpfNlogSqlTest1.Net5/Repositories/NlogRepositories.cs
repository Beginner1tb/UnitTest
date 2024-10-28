using NLog;
using WpfNlogSqlTest1.Net5.Interfaces;

namespace WpfNlogSqlTest1.Net5.Repositories
{
    public class NlogRepositories: INlogRepositories
    {
        private static readonly Logger _logger=LogManager.GetCurrentClassLogger();
        public void LogInfo(string message)
        {
            _logger.Info(message);
        }
    }
}