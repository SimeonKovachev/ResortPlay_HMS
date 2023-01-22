using Microsoft.AspNet.Identity.EntityFramework;
using ResortPlay.Entity;
using ResortPlay.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResortPlay.Areas.Dashboard.ViewModels
{
    public class UserListingModel
    {
        public IEnumerable<HMS_User> Users { get; set; }

        public string RoleId { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }

    }
    public class UserActionModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

    }
    public class UserRolesModel
    {
        public IEnumerable<IdentityRole> UserRoles { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }


    }
}