using System;
using System.Collections.Generic;
using System.Text;
using WpfInterfaceSqlTest.Models;
using WpfInterfaceSqlTest.Interfaces;
using WpfInterfaceSqlTest.Repositories;

namespace WpfInterfaceSqlTest.Services
{
    public class DataBaseServices
    {
        private readonly IUserRepositories _userRepositories;
        public DataBaseServices(IUserRepositories userRepositories)
        {
            _userRepositories = userRepositories;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepositories.GetAllUsers();
        }

        public int GetPriorityNum(string username)
        {
            return _userRepositories.GetPriorityNum(username);
        }
    }
}
