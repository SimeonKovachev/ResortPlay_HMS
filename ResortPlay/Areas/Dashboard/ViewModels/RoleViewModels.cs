using Microsoft.AspNet.Identity.EntityFramework;
using ResortPlay.Entity;
using ResortPlay.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortPlay.Areas.Dashboard.ViewModels
{
    public class RoleListingModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }

    }
    public class RoleActionModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }
}