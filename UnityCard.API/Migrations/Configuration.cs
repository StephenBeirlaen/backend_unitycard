using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UnityCard.BusinessLayer.Context;
using UnityCard.API.Helpers;
using UnityCard.Models;

namespace UnityCard.API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<UnityCardDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UnityCardDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            // Rollen aanmaken
            using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)))
            {
                if (!roleManager.RoleExists(ApplicationRoles.RETAILER))
                    roleManager.Create(new IdentityRole(ApplicationRoles.RETAILER));
                if (!roleManager.RoleExists(ApplicationRoles.CUSTOMER))
                    roleManager.Create(new IdentityRole(ApplicationRoles.CUSTOMER));
            }

            // ApplicationUsers aanmaken
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Email.Equals("stephen.beirlaen@student.howest.be"));
            if (user == null)
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                user = new ApplicationUser()
                {
                    LastName = "Beirlaen",
                    FirstName = "Stephen",
                    Email = "stephen.beirlaen@student.howest.be",
                    UserName = "stephen.beirlaen@student.howest.be",
                    Language = "nl-BE",
                    DisableNotifications = false
                };

                manager.Create(user, "-Password1");
                manager.AddToRole(user.Id, ApplicationRoles.CUSTOMER);
            }

            // Dummy data invoeren

            // LoyaltyCards
            var loyaltyCards = new List<LoyaltyCard>
            {
                new LoyaltyCard(user.Id)
            };
            loyaltyCards.ForEach(t => context.LoyaltyCards.AddOrUpdate<LoyaltyCard>(o => o.UserId, t));
            context.SaveChanges();

            // RetailerCategories
            var retailerCategories = new List<RetailerCategory>
            {
                new RetailerCategory("Supermarkt"),
                new RetailerCategory("Pastabar"),
                new RetailerCategory("Kleerwinkel")
            };
            //retailerCategories.ForEach(t => context.RetailerCategories.Add(t));                                
            retailerCategories.ForEach(t => context.RetailerCategories.AddOrUpdate<RetailerCategory>(o => o.Name, t));
            context.SaveChanges();

            // Retailers
            var retailers = new List<Retailer>
            {
                new Retailer(retailerCategories[0].Id, "Carrefour", "Lage prijzen, plezier inbegrepen", true, "http://hyper.carrefour.eu/sites/hyper.carrefour.eu/files/hyper-nl_0_0.png"),
                new Retailer(retailerCategories[0].Id, "Intermarch�", "Tous unis contre la vie ch�re", true, "http://franchisingbelgium.be/wp-content/uploads/2016/02/Intermarch%C3%A9_Logo-1.png"),
                new Retailer(retailerCategories[1].Id, "Pastaciutta", "Tagline", true, ""),
                new Retailer(retailerCategories[2].Id, "Clovis", "Tagline", false, "http://clovis.be/wp-content/themes/clovis/img/logo-clovis-small.png")
            };
            retailers.ForEach(t => context.Retailers.AddOrUpdate<Retailer>(o => o.Name, t));
            context.SaveChanges();

            // LoyaltyPoints
            var loyaltyPoints = new List<LoyaltyPoint>
            {
                new LoyaltyPoint(loyaltyCards[0].Id, retailers[1].Id, 13),
                new LoyaltyPoint(loyaltyCards[0].Id, retailers[2].Id, 5),
                new LoyaltyPoint(loyaltyCards[0].Id, retailers[3].Id, 8)
            };
            loyaltyPoints.ForEach(t => context.LoyaltyPoints.AddOrUpdate<LoyaltyPoint>(o => o.RetailerId, t));
            context.SaveChanges();

            // RetailerLocations
            var retailerLocations = new List<RetailerLocation>
            {
                new RetailerLocation(retailers[1].Id, "Intermarch� Waregem", 50.888373, 3.442301, "Hippodroomstraat", "45", 8790, "Waregem", "Belgi�"),
                new RetailerLocation(retailers[0].Id, "Carrefour Sint-Eloois-Vijve", 50.898427, 3.401234, "Gentseweg", "524", 8793, "Sint-Eloois-Vijve", "Belgi�"),
                new RetailerLocation(retailers[0].Id, "Carrefour Express Kortrijk", 50.828343, 3.264955, "Leiestraat", "12", 8500, "Kortrijk", "Belgi�"),
                new RetailerLocation(retailers[3].Id, "Clovis", 50.8270158, 3.2717236, "Sint-Jansstraat", "15", 8500, "Kortrijk", "Belgi�")
            };
            retailerLocations.ForEach(t => context.RetailerLocations.AddOrUpdate<RetailerLocation>(o => o.Name, t));
            context.SaveChanges();

            // Offers
            var offers = new List<Offer>
            {
                new Offer(retailers[0].Id, "Per 50 spaarpunten", "5 euro korting!"),
                new Offer(retailers[1].Id, "Per aankoopschijf van 100 euro", "10 spaarpunten cadeau"),
                new Offer(retailers[2].Id, "Bij 20 spaarpunten", "gratis small pasta"),
                new Offer(retailers[3].Id, "Bij aankoop van 100 euro", "gratis set oorbellen")
            };
            offers.ForEach(t => context.Offers.AddOrUpdate<Offer>(o => o.RetailerId, t));
            context.SaveChanges();
        }
    }
}
