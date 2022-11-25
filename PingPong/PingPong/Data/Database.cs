using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingPong.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection connection;

        public Database(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
            connection.CreateTableAsync<Match>().Wait();
        }

        public List<Match> GetAllMatches()
        {
            return connection.Table<Match>().ToListAsync().Result;
        }

        public void SaveMatchData(Match match)
        {
            connection.InsertAsync(match);
        }

        public Match GetLastMatch()
        {
            return connection.Table<Match>().OrderByDescending(x => x.Idopont).FirstAsync().Result;
        }


    }
}
