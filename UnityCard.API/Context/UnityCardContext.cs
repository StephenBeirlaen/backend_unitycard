using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using UnityCard.Models;

namespace UnityCard.API.Context
{
    public class UnityCardContext : IdentityDbContext<ApplicationUser>
    {
        public UnityCardContext()
            : base("UnityCardConnection", throwIfV1Schema: false)
        {
        }

        public static UnityCardContext Create()
        {
            return new UnityCardContext();
        }

        public DbSet<LoyaltyCard> LoyaltyCards { get; set; }

        public DbSet<LoyaltyPoint> LoyaltyPoints { get; set; }

        public DbSet<CustomerJunction> CustomerJunctions { get; set; }

        public DbSet<Retailer> Retailers { get; set; }

        public DbSet<RetailerLocation> RetailerLocations { get; set; }

        public DbSet<RetailerCategory> RetailerCategories { get; set; }

        public DbSet<Offer> Offers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });*/

            /*modelBuilder.Entity<LoyaltyCard>()
              .HasRequired(lc => lc.User)
              .WithRequiredDependent(au => au.LoyaltyCard)
              .HasForeignKey(cj => cj.UserId);*/

            /*modelBuilder.Entity<LoyaltyCard>()
                .HasRequired(lc => lc.User)
                .WithRequiredDependent(au => au.LoyaltyCard);*/ // werkte zonder errors
            
            


            modelBuilder.Entity<CustomerJunction>()
                .HasRequired<LoyaltyCard>(cj => cj.LoyaltyCard)
                .WithMany(lc => lc.CustomerJunctions)
                .HasForeignKey(cj => cj.LoyaltyCardId);

            modelBuilder.Entity<CustomerJunction>()
                .HasRequired<Retailer>(cj => cj.Retailer)
                .WithMany(r => r.CustomerJunctions)
                .HasForeignKey(cj => cj.RetailerId);

            modelBuilder.Entity<LoyaltyPoint>()
                .HasRequired<CustomerJunction>(lp => lp.CustomerJunction) // LoyaltyPoint entity requires CustomerJunction 
                .WithMany(cj => cj.LoyaltyPoints) // CustomerJunction entity includes many LoyaltyPoints entities
                .HasForeignKey(lp => lp.CustomerJunctionId);

            modelBuilder.Entity<Retailer>()
                .HasRequired<RetailerCategory>(r => r.RetailerCategory)
                .WithMany(rc => rc.Retailers)
                .HasForeignKey(r => r.RetailerCategoryId);

            modelBuilder.Entity<Offer>()
                .HasRequired<Retailer>(o => o.Retailer)
                .WithMany(r => r.Offers)
                .HasForeignKey(o => o.RetailerId);

            modelBuilder.Entity<RetailerLocation>()
                .HasRequired<Retailer>(rl => rl.Retailer)
                .WithMany(r => r.RetailerLocations)
                .HasForeignKey(rl => rl.RetailerId);
        }
    }
}
