using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace _2NiteAHI
{
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string email { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string pNumber { get; set; }
    }
}
