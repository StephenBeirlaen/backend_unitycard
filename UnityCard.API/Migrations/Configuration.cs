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
            ApplicationUser customer1 = context.Users.FirstOrDefault(u => u.Email.Equals("customer1@unitycard.com"));
            if (customer1 == null)
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                customer1 = new ApplicationUser()
                {
                    LastName = "Demo 1",
                    FirstName = "Customer",
                    Email = "customer1@unitycard.com",
                    UserName = "customer1@unitycard.com",
                    Language = "nl-BE",
                    DisableNotifications = false
                };

                manager.Create(customer1, "-Password1");
                manager.AddToRole(customer1.Id, ApplicationRoles.CUSTOMER);
            }
            ApplicationUser retailer1 = context.Users.FirstOrDefault(u => u.Email.Equals("retailer1@unitycard.com"));
            if (retailer1 == null)
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                retailer1 = new ApplicationUser()
                {
                    LastName = "Demo 1",
                    FirstName = "Retailer",
                    Email = "retailer1@unitycard.com",
                    UserName = "retailer1@unitycard.com",
                    Language = "nl-BE",
                    DisableNotifications = false
                };

                manager.Create(retailer1, "-Password1");
                manager.AddToRole(retailer1.Id, ApplicationRoles.RETAILER);
            }

            // Dummy data invoeren

            // LoyaltyCards
            var loyaltyCards = new List<LoyaltyCard>
            {
                new LoyaltyCard(customer1.Id)
            };
            loyaltyCards.ForEach(t => context.LoyaltyCards.AddOrUpdate<LoyaltyCard>(o => o.UserId, t));
            context.SaveChanges();

            // RetailerCategories
            var retailerCategories = new List<RetailerCategory>
            {
                new RetailerCategory("Supermarkt"),
                new RetailerCategory("Pastabar"),
                new RetailerCategory("Kleerwinkel"),
                new RetailerCategory("Doe-het-zelfzaak"),
                new RetailerCategory("Hamburgerrestaurant"),
                new RetailerCategory("Fastfoodketen")
            };
            //retailerCategories.ForEach(t => context.RetailerCategories.Add(t));                                
            retailerCategories.ForEach(t => context.RetailerCategories.AddOrUpdate<RetailerCategory>(o => o.Name, t));
            context.SaveChanges();

            // Retailers
            var retailers = new List<Retailer>
            {
                new Retailer(retailerCategories[0].Id, "Carrefour", "Lage prijzen, plezier inbegrepen", true, "http://www.bloovi.be/frontend/files/blog/images/source/mobilosoft-maakt-mobiele-sites-van-carrefour.jpg"),
                new Retailer(retailerCategories[0].Id, "Intermarché", "Tous unis contre la vie chère", true, "http://franchisingbelgium.be/wp-content/uploads/2016/02/Intermarch%C3%A9_Logo-1.png"),
                new Retailer(retailerCategories[1].Id, "Pastaciutta", "Tagline", true, "https://scontent-amt2-1.xx.fbcdn.net/v/t1.0-9/557608_10150702296215969_2428331_n.jpg?oh=53e2f2b49a0977265d4fd58ab01604e0&oe=5914718C"),
                new Retailer(retailerCategories[2].Id, "Clovis", "Tagline", false, "http://clovis.be/wp-content/themes/clovis/img/logo-clovis-small.png"),
                new Retailer(retailerCategories[3].Id, "Brico", "Voor de makers", true, "https://lh3.googleusercontent.com/-ia1zDysHhng/V0K1iz-fz1I/AAAAAAAAAAk/r_9whdbEutwQLO4bAQI6ybCraOtbzN_hgCJkC/s408-k-no/"),
                new Retailer(retailerCategories[0].Id, "Spar", "Altijd in je buurt!", true, "http://www.gondola.be/sites/default/files/news_aktualiteits_artikel/spar_help_portrait_yes-we-care.jpg"),
                new Retailer(retailerCategories[4].Id, "Paul's Boutique", "Original, Delicious, Fresh, Rock&Roll", true, "http://www.paulsboutique.be/images/logover.png"),
                new Retailer(retailerCategories[5].Id, "McDonalds's", "I'm lovin' it", true, "https://yt3.ggpht.com/-avTHbIvvjKY/AAAAAAAAAAI/AAAAAAAAAAA/GtO4B-SrWkA/s900-c-k-no-mo-rj-c0xffffff/photo.jpg")
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
                new RetailerLocation(retailers[1].Id, "Intermarché Waregem", "Hippodroomstraat", "45", 8790, "Waregem", "België"),
                new RetailerLocation(retailers[0].Id, "Carrefour Sint-Eloois-Vijve", "Gentseweg", "524", 8793, "Sint-Eloois-Vijve", "België"),
                new RetailerLocation(retailers[0].Id, "Carrefour Express Kortrijk", "Leiestraat", "12", 8500, "Kortrijk", "België"),
                new RetailerLocation(retailers[3].Id, "Clovis", "Sint-Jansstraat", "15", 8500, "Kortrijk", "België"),
                new RetailerLocation(retailers[4].Id, "Brico Waregem", "Wijmeriestraat", "13", 8790, "Waregem", "België"),
                new RetailerLocation(retailers[4].Id, "Brico Plan-it Kortrijk", "Engelse Wandeling", "2", 8500, "Kortrijk", "België"),
                new RetailerLocation(retailers[5].Id, "Spar Kortrijk", "Engelse Wandeling", "1", 8500, "Kortrijk", "België"),
                new RetailerLocation(retailers[5].Id, "Spar Waregem Holstraat", "Holstraat", "78", 8790, "Waregem", "België"),
                new RetailerLocation(retailers[5].Id, "Spar Waregem Servaeslaan", "Albert Servaeslaan", "48", 8790, "Waregem", "België"),
                new RetailerLocation(retailers[6].Id, "Paul's Fresh Food Boutique", "Kleine Sint-Jansstraat", "15", 8500, "Kortrijk", "België"),
                new RetailerLocation(retailers[6].Id, "Jill's Boutique", "Zandstraat", "19", 8500, "Kortrijk", "België"),
                new RetailerLocation(retailers[7].Id, "McDonald's Kortrijk", "President Kennedylaan", "100", 8500, "Kortrijk", "België")
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
