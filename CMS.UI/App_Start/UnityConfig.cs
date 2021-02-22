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


            //Here, container.RegisterType<IUnitOfWork, UnitOfWork>() requests Unity to create an object of the UnitOfWork class and inject it through a constructor whenever you need to inject an object of IUnitOfWork.

            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.AddNewExtension<Log4NetExtension>();
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}