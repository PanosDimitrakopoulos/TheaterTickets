using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheaterTickets.classes
{
    public class Play
    {
        public string id { get; set; }
        public string name { get; set; }
        public string day_time { get; set; }
        public string shortinfo { get; set; }
        public string seatratio { get; set; }
        public string price { get; set; }
    }

    public class Plays
    {
        public List<Play> play { get; set; }
    }

    public class RootPlay
    {
        public Plays plays { get; set; }
    }
}
