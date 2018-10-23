using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TPS.Models;

namespace TPS.Secuirty
{
    public class ApplicationSignInManager : SignInManager<UserLogin, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {

        }
    }
}