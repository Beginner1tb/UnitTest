using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
        
        public User GetUserInfo(string username)
        {
            return _userRepositories.GetUserInfo(username);
        }

        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _userRepositories.AddUser(user);
        }

        private string GetMd5Hash(string strUserPassword)
        {
            using var md5 = MD5.Create();
            var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(strUserPassword));
            var sb = new StringBuilder();
            for (int i = 0; i <hashBytes.Length ; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public void DeleteUser(User user)
        {
            _userRepositories.DeleteUser(user);
        }
        public void UpdateUser(User user)
        {
            _userRepositories.UpdateUser(user);
        }
        
    }
}
