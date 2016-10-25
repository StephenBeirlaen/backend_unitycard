using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UnityCard.API.Context;
using UnityCard.API.Helpers;
using UnityCard.Models;

namespace UnityCard.API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<UnityCardContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UnityCardContext context)
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
                new LoyaltyCard {CreatedTimestamp = DateTime.Now, UserId = user.Id}
            };
            loyaltyCards.ForEach(t => context.LoyaltyCards.AddOrUpdate<LoyaltyCard>(o => o.UserId, t));
            context.SaveChanges();

            // RetailerCategories
            var retailerCategories = new List<RetailerCategory>
            {
                new RetailerCategory {Name = "Supermarkt"},
                new RetailerCategory {Name = "Pastabar"},
                new RetailerCategory {Name = "Kleerwinkel"}
            };
            //retailerCategories.ForEach(t => context.RetailerCategories.Add(t));                                
            retailerCategories.ForEach(t => context.RetailerCategories.AddOrUpdate<RetailerCategory>(o => o.Name, t));
            context.SaveChanges();

            // Retailers
            var retailers = new List<Retailer>
            {
                new Retailer {RetailerCategoryId = retailerCategories[0].Id, Chain = true, Name = "Carrefour", Tagline = "Lage prijzen, plezier inbegrepen", LogoUrl = "http://hyper.carrefour.eu/sites/hyper.carrefour.eu/files/hyper-nl_0_0.png"},
                new Retailer {RetailerCategoryId = retailerCategories[0].Id, Chain = true, Name = "Intermarché", Tagline = "Tous unis contre la vie chère", LogoUrl = "http://franchisingbelgium.be/wp-content/uploads/2016/02/Intermarch%C3%A9_Logo-1.png"},
                new Retailer {RetailerCategoryId = retailerCategories[1].Id, Chain = true, Name = "Pastaciutta", Tagline = "Tagline", LogoUrl = ""},
                new Retailer {RetailerCategoryId = retailerCategories[2].Id, Chain = false, Name = "Clovis", Tagline = "Tagline", LogoUrl = "http://clovis.be/wp-content/themes/clovis/img/logo-clovis-small.png"}
            };
            retailers.ForEach(t => context.Retailers.AddOrUpdate<Retailer>(o => o.Name, t));
            context.SaveChanges();

            // CustomerJunctions
            var customerJunctions = new List<CustomerJunction>
            {
                new CustomerJunction {LoyaltyCardId = loyaltyCards[0].Id, RetailerId = retailers[1].Id},
                new CustomerJunction {LoyaltyCardId = loyaltyCards[0].Id, RetailerId = retailers[2].Id},
                new CustomerJunction {LoyaltyCardId = loyaltyCards[0].Id, RetailerId = retailers[3].Id}
            };
            customerJunctions.ForEach(t => context.CustomerJunctions.AddOrUpdate<CustomerJunction>(o => o.RetailerId, t));
            context.SaveChanges();

            // LoyaltyPoints
            var loyaltyPoints = new List<LoyaltyPoint>
            {
                new LoyaltyPoint {CustomerJunctionId = customerJunctions[0].Id, Value = 13, Timestamp = DateTime.Now},
                new LoyaltyPoint {CustomerJunctionId = customerJunctions[1].Id, Value = 5, Timestamp = DateTime.Now},
                new LoyaltyPoint {CustomerJunctionId = customerJunctions[2].Id, Value = 8, Timestamp = DateTime.Now}
            };
            loyaltyPoints.ForEach(t => context.LoyaltyPoints.AddOrUpdate<LoyaltyPoint>(o => o.CustomerJunctionId, t));
            context.SaveChanges();

            // RetailerLocations
            var retailerLocations = new List<RetailerLocation>
            {
                new RetailerLocation {RetailerId = retailers[1].Id, Name = "Intermarché Waregem", Latitude = 50.888373, Longitude = 3.442301, Street = "Hippodroomstraat", Number = "45", ZipCode = 8790, City = "Waregem", Country = "België"},
                new RetailerLocation {RetailerId = retailers[0].Id, Name = "Carrefour Sint-Eloois-Vijve", Latitude = 50.898427, Longitude = 3.401234, Street = "Gentseweg", Number = "524", ZipCode = 8793, City = "Sint-Eloois-Vijve", Country = "België"},
                new RetailerLocation {RetailerId = retailers[0].Id, Name = "Carrefour Express Kortrijk", Latitude = 50.828343, Longitude = 3.264955, Street = "Leiestraat", Number = "12", ZipCode = 8500, City = "Kortrijk", Country = "België"},
                new RetailerLocation {RetailerId = retailers[3].Id, Name = "Clovis", Latitude = 50.8270158, Longitude = 3.2717236, Street = "Sint-Jansstraat", Number = "15", ZipCode = 8500, City = "Kortrijk", Country = "België"}
            };
            retailerLocations.ForEach(t => context.RetailerLocations.AddOrUpdate<RetailerLocation>(o => o.Name, t));
            context.SaveChanges();

            // Offers
            var offers = new List<Offer>
            {
                new Offer {RetailerId = retailers[0].Id, OfferDemand = "Per 50 spaarpunten", OfferReceive = "5 euro korting!", CreatedTimestamp = DateTime.Now},
                new Offer {RetailerId = retailers[1].Id, OfferDemand = "Per aankoopschijf van 100 euro", OfferReceive = "10 spaarpunten cadeau", CreatedTimestamp = DateTime.Now},
                new Offer {RetailerId = retailers[2].Id, OfferDemand = "Bij 20 spaarpunten", OfferReceive = "gratis small pasta", CreatedTimestamp = DateTime.Now},
                new Offer {RetailerId = retailers[3].Id, OfferDemand = "Bij aankoop van 100 euro", OfferReceive = "gratis set oorbellen", CreatedTimestamp = DateTime.Now}
            };
            offers.ForEach(t => context.Offers.AddOrUpdate<Offer>(o => o.RetailerId, t));
            context.SaveChanges();
        }
    }
}
