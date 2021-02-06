using CMS.Repository;
using System.Web.Mvc;
using Unity;
using Unity.log4net;
using Unity.Mvc5;

namespace CMS.UI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.AddNewExtension<Log4NetExtension>();
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}