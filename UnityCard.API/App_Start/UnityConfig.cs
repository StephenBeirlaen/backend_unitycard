using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using UnityCard.API.Controllers;
using UnityCard.BusinessLayer.Repositories;
using UnityCard.BusinessLayer.Repositories.Interfaces;

namespace UnityCard.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<AccountController>(new InjectionConstructor());

            container.RegisterType<ILoyaltyCardRepository, LoyaltyCardRepository>();
            container.RegisterType<ILoyaltyPointRepository, LoyaltyPointRepository>();
            container.RegisterType<IOfferRepository, OfferRepository>();
            container.RegisterType<IRetailerCategoryRepository, RetailerCategoryRepository>();
            container.RegisterType<IRetailerLocationRepository, RetailerLocationRepository>();
            container.RegisterType<IRetailerRepository, RetailerRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}