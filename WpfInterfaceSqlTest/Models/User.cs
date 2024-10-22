using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace WpfInterfaceSqlTest.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
