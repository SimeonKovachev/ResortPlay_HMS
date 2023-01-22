using ResortPlay.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortPlay.Areas.Dashboard.ViewModels
{
    public class AccomodationTypesListingModel
    {
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
        public string SearchTerm { get;  set; }
    }

    public class AccomodationTypeActionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

}