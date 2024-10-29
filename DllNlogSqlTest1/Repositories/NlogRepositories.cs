using DllNlogTest1.Interfaces;
using NLog;

namespace DllNlogTest1.Repositories
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