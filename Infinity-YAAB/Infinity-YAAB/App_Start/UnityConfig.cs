using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

namespace Infinity_YAAB
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}