using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using _2NiteAHI.Droid;
using _2NiteAHI.Interfaces;
using SQLite;
using Xamarin.Forms;

namespace _2NiteAHI.Droid
{
    public class ISQLiteDBInterface_Android : ISQLiteInterface
    {
        public ISQLiteDBInterface_Android() { }
        public SQLite.SQLiteConnection GetSQLiteConnection()
        {
            var fileName = "UserDatabase.db3";
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, fileName);
            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}