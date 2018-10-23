using Microsoft.AspNet.Identity;
using TPS.Models;

namespace TPS.Secuirty
{
    public class ApplicationUserManager : UserManager<UserLogin>
    {
        public ApplicationUserManager(IUserStore<UserLogin> userStore) : base(userStore)
        {

        }
    }
}