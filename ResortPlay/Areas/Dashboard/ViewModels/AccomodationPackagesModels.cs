﻿using ResortPlay.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortPlay.Areas.Dashboard.ViewModels
{
    public class AccomodationPackagesListingModel
    {
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
        public string SearchTerm { get; set; }
    }
    public class AccomodationPackageActionModel
    {
        public int Id { get; set; }
        public int AccomodationTypeId { get; set; }
        public AccomodationType AccomodationType { get; set; }

        public string Name { get; set; }
        public int NoOfRooms { get; set; }
        public decimal FeePerNight { get; set; }
    }

}