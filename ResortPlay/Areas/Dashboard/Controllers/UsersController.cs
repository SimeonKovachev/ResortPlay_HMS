using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ResortPlay.Areas.Dashboard.ViewModels;
using ResortPlay.Entity;
using ResortPlay.ViewModels;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ResortPlay.Areas.Dashboard.Controllers
{
    public class UsersController : Controller
    {

        private HMSSignInManager _signInManager;
        private HMSUserManager _userManager;
        private HMSRoleManager _roleManager;

        public HMSSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<HMSSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public HMSUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<HMSUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public HMSRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<HMSRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        } 

        public UsersController()
        {
        }

        public UsersController(HMSUserManager userManager, HMSSignInManager signInManager, HMSRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public ActionResult Index(string searchTerm, string roleId, int? page) //search by seartchterm and accomodation package by its id. Only gets int.  // The int page is for the pages each with 3 packages
        {
            //how much accomodations will show at a time
            int recordSize = 10;
            page = page ?? 1;

            UserListingModel model = new UserListingModel();
            //Word search
            model.SearchTerm = searchTerm;
            //Type search
            model.RoleId = roleId;
            model.Roles = RoleManager.Roles.ToList();

            model.Users = SearchUsers(searchTerm, roleId, page.Value, recordSize);


            //The pager controller
            var totalRecords = SearchUsersCount(searchTerm, roleId);
            model.Pager = new Pager(totalRecords, page, recordSize);
            return View(model);
        }
        //Search Users
        public IEnumerable<HMS_User> SearchUsers(string searchTerm, string roleId, int page, int recordSize)
        {

            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }

            if (!string.IsNullOrEmpty(roleId))
            {
                //users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }
            var skip = (page - 1) * recordSize;
          

            return users.OrderBy(x => x.Email).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchUsersCount(string searchTerm, string roleId)
        {

            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }

            if (!string.IsNullOrEmpty(roleId))
            {
                //users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }
            return users.Count();
        }




        //Here to Read from the model
        [HttpGet]
        public async Task<ActionResult> Action(string Id)
        {
            UserActionModel model = new UserActionModel();

            if (!string.IsNullOrEmpty(Id)) // View the records
            {
                var user = await UserManager.FindByIdAsync(Id);

                model.Id = user.Id;
                model.FullName = user.FullName;
                model.Email = user.Email;
                model.Username = user.UserName;
                model.Country = user.Country;
                model.City = user.City;
                model.Address = user.Address;

            }

            return PartialView("_Action", model);
        }
        //Here to make an action
        [HttpPost]
        public async Task<JsonResult> Action(UserActionModel model)
        {

            JsonResult json = new JsonResult();
            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.Id)) //Edit a record
            {
                var user = await UserManager.FindByIdAsync(model.Id);

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.Username;

                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;

                result = await UserManager.UpdateAsync(user);
            }
            else //Create a record
            {

                var user = new HMS_User();

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.Username;

                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;

                result = await UserManager.CreateAsync(user);
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors) };
            return json;
        }
        //Here is to Delete the accomodation
        [HttpGet]
        public async Task<ActionResult> Delete(string Id)
        {
            UserActionModel model = new UserActionModel();

            var user = await UserManager.FindByIdAsync(Id);

            model.Id = user.Id;

            return PartialView("_Delete", model);

        }
        [HttpPost]
        public async Task<JsonResult> Delete(UserActionModel model)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;

            var user = await UserManager.FindByIdAsync(model.Id);
            result = await UserManager.DeleteAsync(user);

            json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors) };
            return json;
        }


        //Here to Read from the model
        [HttpGet]
        public async Task<ActionResult> UserRoles(string Id)
        {
            
            UserRolesModel model = new UserRolesModel();
            model.Roles = RoleManager.Roles.ToList();

            var user = await UserManager.FindByIdAsync(Id);
            var UserRolesIds = user.Roles.Select(x => x.RoleId).ToList();
            model.UserRoles = RoleManager.Roles.Where(x => UserRolesIds.Contains(x.Id)).ToList();

            return PartialView("_UserRoles", model);
        }
        //Here to make an action
        [HttpPost]
        public async Task<JsonResult> UserRoles(UserActionModel model)
        {

            JsonResult json = new JsonResult();
            IdentityResult result = null;

            if (!string.IsNullOrEmpty(model.Id)) //Edit a record
            {
                var user = await UserManager.FindByIdAsync(model.Id);

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.Username;

                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;

                result = await UserManager.UpdateAsync(user);
            }
            else //Create a record
            {

                var user = new HMS_User();

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.Username;

                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;

                result = await UserManager.CreateAsync(user);
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors) };
            return json;
        }
    }
}