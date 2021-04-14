using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace _2NiteAHI.Interfaces
{
    public interface ISQLiteInterface
    {
        SQLiteConnection GetSQLiteConnection();
    }
}
