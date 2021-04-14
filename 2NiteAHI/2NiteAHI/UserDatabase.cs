using System;
using System.Collections.Generic;
using System.Text;
using _2NiteAHI.Interfaces;
using SQLite;
using System.Linq;
using Xamarin.Forms;

namespace _2NiteAHI.Helper
{
    public class UserDatabase
    {
        private SQLiteConnection _SQLiteConnection;
        public UserDatabase()
        {
            _SQLiteConnection = DependencyService.Get<ISQLiteInterface>().GetSQLiteConnection();
            _SQLiteConnection.CreateTable<Users>();
        }
        public IEnumerable<Users> GetUsers()
        {
            return (from u in _SQLiteConnection.Table<Users>()
                    select u).ToList();
        }
        public Users GetSpecificUser(int id)
        {
            return _SQLiteConnection.Table<Users>().FirstOrDefault(t => t.ID == id);
        }
        public void DeleteUser(int id)
        {
            _SQLiteConnection.Delete<Users>(id);
        }
        public string AddUser(Users user)
        {
            var data = _SQLiteConnection.Table<Users>();
            var d1 = data.Where(x => x.username == user.username && x.username == user.username).FirstOrDefault();
            if (d1 == null)
            {
                _SQLiteConnection.Insert(user);
                return "Registered";
            }
            else
                return "Username already registered...";
        }
        public bool updateUserValidation(string userid)
        {
            var data = _SQLiteConnection.Table<Users>();
            var d1 = (from values in data
                      where values.username == userid
                      select values).Single();
            if (d1 != null)
            {
                return true;
            }
            else { return false; }
        }
        public bool updateUser(String uname, string pwd)
        {
            var data = _SQLiteConnection.Table<Users>();
            var d1 = (from values in data
                      where values.username == uname
                      select values).Single();
            if (true)
            {
                d1.password = pwd;
                _SQLiteConnection.Update(d1);
                return true;
            }
            else { return false; }
        }
        public bool LoginValidate(string uName1, string pwd1)
        {
            var data = _SQLiteConnection.Table<Users>();
            var d1 = data.Where(x => x.username == uName1 && x.password == pwd1).FirstOrDefault();
            if(d1 != null)
            {
                return true;
            }
            else { return false; }
        }


    }
}
