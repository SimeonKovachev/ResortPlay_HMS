using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortPlay.Entity
{
    public class AccomodationType
    {
        public int Id { get; set; }
        public string Name { get; set; } //Hotel Room, aparment room
        public string Description { get; set; } //Description for the hotel room or apartment
    }
}
