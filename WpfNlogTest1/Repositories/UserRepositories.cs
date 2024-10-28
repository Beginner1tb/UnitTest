using System;
using System.Collections.Generic;
using System.Text;
using WpfNlogTest1.Interfaces;
using NLog;

namespace WpfNlogTest1.Repositories
{
    public class UserRepositories : IUserRepositories
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        public void LogInfo(string message)
        {
            _logger.Info(message);
        }
    }
}
