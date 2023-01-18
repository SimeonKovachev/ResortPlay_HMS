﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortPlay.Entity
{
    public class AccomodationPackage
    {
        public int Id { get; set; }

        public int AccomodationTypeId { get; set; }
        public virtual AccomodationType AccomodationType { get; set; }

        public string Name { get; set; }
        public int NoOfRooms { get; set; }
        public decimal FeePerNight { get; set; }


    }
}
