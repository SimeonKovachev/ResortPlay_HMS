using Microsoft.Ajax.Utilities;
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
using System.Web.UI;

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

        public async Task<ActionResult> Index(string searchTerm, string roleId, int? page) 
        {
            //how much will show at a time
            int recordSize = 10;
            page = page ?? 1;

            UserListingModel model = new UserListingModel();
            //Word search
            model.SearchTerm = searchTerm;
            model.RoleId = roleId;
            model.Roles = RoleManager.Roles.ToList();

            model.Users = await SearchUsers(searchTerm, roleId, page.Value, recordSize);


            //The pager controller
            var totalRecords = await SearchUsersCount(searchTerm, roleId);
            model.Pager = new Pager(totalRecords, page, recordSize);
            return View(model);
        }
        //Search Users
        public async Task<IEnumerable<HMS_User>> SearchUsers(string searchTerm, string roleId, int page, int recordSize)
        {

            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }

            if (!string.IsNullOrEmpty(roleId))
            {
                var role = await RoleManager.FindByIdAsync(roleId);

                var userIds = role.Users.Select(x => x.UserId).ToList();
                users = users.Where(x => userIds.Contains(x.Id));
            }
            var skip = (page - 1) * recordSize;
          

            return users.OrderBy(x => x.Email).Skip(skip).Take(recordSize).ToList();
        }

        public async Task<int> SearchUsersCount(string searchTerm, string roleId)
        {

            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }

            if (!string.IsNullOrEmpty(roleId))
            {
                var role = await RoleManager.FindByIdAsync(roleId);

                var userIds = role.Users.Select(x => x.UserId).ToList();
                users = users.Where(x => userIds.Contains(x.Id));
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


        //The User Roles Controller. Taking id from the roles and shows the roles
        [HttpGet]
        public async Task<ActionResult> UserRoles(string Id)
        {
            
            UserRolesModel model = new UserRolesModel();

            model.UserId = Id;
            var user = await UserManager.FindByIdAsync(Id);
            var UserRolesIds = user.Roles.Select(x => x.RoleId).ToList();
            model.UserRoles = RoleManager.Roles.Where(x => UserRolesIds.Contains(x.Id)).ToList();

            model.Roles = RoleManager.Roles.Where(x => !UserRolesIds.Contains(x.Id)).ToList();

            return PartialView("_UserRoles", model);
        }
        //Here to make an action
        [HttpPost]
        public async Task<JsonResult> UserRoleOperation(string userId, string roleId, bool isDelete = false)
        {
            JsonResult json = new JsonResult();
            var user = await UserManager.FindByIdAsync(userId);
            var role = await RoleManager.FindByIdAsync(roleId);

            if (user != null && role != null)
            {
                IdentityResult result = null;
                if (!isDelete)
                {
                    result = await UserManager.AddToRoleAsync(userId, role.Name);
                }
                else
                {
                    result = await UserManager.RemoveFromRoleAsync(userId, role.Name);
                }
                
                json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors)};
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid operation! " };
            }

            return json;

        }
    }
}