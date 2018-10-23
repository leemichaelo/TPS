[assembly: WebActivator.PostApplicationStartMethod(typeof(TPS.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace TPS.App_Start
{
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin;
    using TPS.Data;
    using TPS.Models;
    using TPS.Secuirty;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;

    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            container.Register<Context>(Lifestyle.Scoped);

            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);

            container.Register(() =>
                container.IsVerifying
                    ? new OwinContext().Authentication
                    : HttpContext.Current.GetOwinContext().Authentication,
                      Lifestyle.Scoped);

            container.Register<IUserStore<UserLogin>>(() =>
                new UserStore<UserLogin>(container.GetInstance<Context>()),
                Lifestyle.Scoped);
        }
    }
}