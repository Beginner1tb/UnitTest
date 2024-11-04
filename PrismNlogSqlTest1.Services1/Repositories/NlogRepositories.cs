
using NLog;
using PrismNlogSqlTest1.Services1.Interfaces;

namespace PrismNlogSqlTest1.Services1.Repositories
{
    public class NlogRepositories: INlogRepositories
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public void LogInfo(string message)
        {
            Logger.Info(message);
        }
    }
}