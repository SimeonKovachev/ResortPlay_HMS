using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using ResortPlay.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class HMSSignInManager : SignInManager<HMS_User, string> 
    {

        public HMSSignInManager(HMSUserManager userManager, IAuthenticationManager authenticationManager)
           : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(HMS_User user)
        {
            return user.GenerateUserIdentityAsync((HMSUserManager)UserManager);
        }

        public static HMSSignInManager Create(IdentityFactoryOptions<HMSSignInManager> options, IOwinContext context)
        {
            return new HMSSignInManager(context.GetUserManager<HMSUserManager>(), context.Authentication);
        }

    }
}
