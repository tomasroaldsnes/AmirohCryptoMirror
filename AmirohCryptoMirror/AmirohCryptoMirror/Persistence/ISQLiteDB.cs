using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmirohCryptoMirror.Persistence
{
    public interface ISQLiteDB
    {
        SQLiteAsyncConnection GetConnection();
    }
}
