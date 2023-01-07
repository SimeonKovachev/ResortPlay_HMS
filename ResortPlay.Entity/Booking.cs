using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortPlay.Entity
{
    public class Booking
    {
        public int Id { get; set; } // Primary Key

        public int AccomodationId { get; set; } //Foreign Key
        public Accomodation Accomodation { get; set; }

        public DateTime FromDate { get; set; }
        public int Duration { get; set; } // No of Nights to stay
    }
}
