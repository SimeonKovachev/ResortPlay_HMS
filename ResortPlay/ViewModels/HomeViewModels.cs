using ResortPlay.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortPlay.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
    }
}