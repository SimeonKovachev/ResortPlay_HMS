using ResortPlay.Entity;
using ResortPlay.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortPlay.Areas.Dashboard.ViewModels
{
    public class AccomodationListingModel
    {
        public IEnumerable<Accomodation> Accomodations { get; set; }

        public int? AccomodationPackageId { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }

    }
    public class AccomodationActionModel
    {
        public int Id { get; set; }

        public int AccomodationPackageId { get; set; }
        public AccomodationPackage AccomodationPackage { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }

    }
}