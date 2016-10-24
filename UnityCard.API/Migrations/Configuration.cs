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

            // 2 gebruikers en 2 rollen aanmaken
            IdentityResult roleResult;

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(ApplicationRoles.RETAILER))
            {
                roleResult = roleManager.Create(new IdentityRole(ApplicationRoles.RETAILER));
            }
            if (!roleManager.RoleExists(ApplicationRoles.CUSTOMER))
            {
                roleResult = roleManager.Create(new IdentityRole(ApplicationRoles.CUSTOMER));
            }

            if (!context.Users.Any(u => u.Email.Equals("stephen.beirlaen@student.howest.be")))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser()
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

            /*LoyaltyCard loyaltyCard = new LoyaltyCard()
            {
                CreatedTimestamp = DateTime.Now,
                User = testUser
            };
            context.LoyaltyCards.AddOrUpdate<LoyaltyCard>(lc => lc.Id, loyaltyCard);*/


            RetailerCategory retailerCategory = new RetailerCategory()
            {
                Name = "Supermarkt"
            };
            context.RetailerCategories.AddOrUpdate<RetailerCategory>(m => m.Name, retailerCategory);

            Retailer retailer = new Retailer()
            {
                RetailerCategoryId = retailerCategory.Id,
                Name = "Testwinkel",
                Chain = false
            };
            context.Retailers.AddOrUpdate<Retailer>(m => m.Name, retailer);
            context.SaveChanges();
        }
    }
}
