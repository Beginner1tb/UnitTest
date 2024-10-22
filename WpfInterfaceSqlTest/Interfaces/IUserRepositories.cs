using System;
using System.Collections.Generic;
using System.Text;
using WpfInterfaceSqlTest.Models;

namespace WpfInterfaceSqlTest.Interfaces
{
    public interface IUserRepositories
    {
        IEnumerable<User> GetAllUsers();
        User GetUserInfo(string username);
        int GetPriorityNum(string username);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
         
    }
}
