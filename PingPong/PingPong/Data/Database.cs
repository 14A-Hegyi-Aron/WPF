using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public bool DeleteMatchData(Match kivalasztott)
        {
            return connection.DeleteAsync(kivalasztott).Result == 1;
        }

        public ObservableCollection<Match> GetAllMatches()
        {
            return new ObservableCollection<Match>(connection.Table<Match>().ToListAsync().Result);
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
