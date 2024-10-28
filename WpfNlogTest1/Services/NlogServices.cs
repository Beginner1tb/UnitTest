using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using WpfNlogTest1.Interfaces;
using WpfNlogTest1.Repositories;


namespace WpfNlogTest1.Services
{
    
    public class NlogServices
    {
        private readonly IUserRepositories _userRepositories;
        public NlogServices(IUserRepositories userRepositories)
        {
            _userRepositories = userRepositories;
        }

        public void LogInfo(string message)
        {
            _userRepositories.LogInfo(message);
        }
    }
}
