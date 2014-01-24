using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheaterTickets.classes
{
    public class Seat
    {
        public string id { get; set; }
        public string number { get; set; }
    }

    public class Seats
    {
        public List<Seat> seat { get; set; }
    }

    public class RootSeat
    {
        public Seats seats { get; set; }
    }
}
