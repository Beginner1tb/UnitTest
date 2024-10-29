using System;
using DllNlogSqlTest1.Interfaces;
using NLog;

namespace DllNlogSqlTest1
{
    public class NlogRepositories: INlogRepositories
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public void LogInfo(string message)
        {
            _logger.Info(message);
        }
    }
}