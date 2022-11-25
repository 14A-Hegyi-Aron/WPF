using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPong
{
    public class Match
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string ElsoJatekosNev { get; set; }
        public string MasodikJatekosNev { get; set; }
        public int ElsoJatekosPont { get; set; }
        public int MasodikJatekosPont { get; set; }
        public DateTime Idopont { get; set; }
        public Match()
        {

        }

  
        public Match(string elsoJatekosNev, string masodikJatekosNev)
        {
            ElsoJatekosNev = elsoJatekosNev;
            MasodikJatekosNev = masodikJatekosNev;
            Idopont = DateTime.Now;
        }

        public Match(string elsoJatekosNev, string masodikJatekosNev, int elsoJatekosPont, int masodikJatekosPont)
        {
            ElsoJatekosNev = elsoJatekosNev;
            MasodikJatekosNev = masodikJatekosNev;
            ElsoJatekosPont = elsoJatekosPont;
            MasodikJatekosPont = masodikJatekosPont;
            Idopont = DateTime.Now;
        }

        public void SetScore(int firstPoint, int secondPoint)
        {
            ElsoJatekosPont = firstPoint;
            MasodikJatekosPont = secondPoint;
        }

    }
}
