namespace UnityCard.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnityCard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerJunctions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoyaltyCardId = c.Int(nullable: false),
                        RetailerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LoyaltyCards", t => t.LoyaltyCardId, cascadeDelete: true)
                .ForeignKey("dbo.Retailers", t => t.RetailerId, cascadeDelete: true)
                .Index(t => t.LoyaltyCardId)
                .Index(t => t.RetailerId);
            
            CreateTable(
                "dbo.LoyaltyCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        CreatedTimestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoyaltyPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        CustomerJunctionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerJunctions", t => t.CustomerJunctionId, cascadeDelete: true)
                .Index(t => t.CustomerJunctionId);
            
            CreateTable(
                "dbo.Retailers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RetailerCategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Tagline = c.String(),
                        Chain = c.Boolean(nullable: false),
                        LogoUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RetailerCategories", t => t.RetailerCategoryId, cascadeDelete: true)
                .Index(t => t.RetailerCategoryId);
            
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RetailerId = c.Int(nullable: false),
                        OfferDemand = c.String(nullable: false),
                        OfferReceive = c.String(nullable: false),
                        CreatedTimestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Retailers", t => t.RetailerId, cascadeDelete: true)
                .Index(t => t.RetailerId);
            
            CreateTable(
                "dbo.RetailerCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RetailerLocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RetailerId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Street = c.String(nullable: false),
                        Number = c.String(nullable: false),
                        ZipCode = c.Int(nullable: false),
                        City = c.String(nullable: false),
                        Country = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Retailers", t => t.RetailerId, cascadeDelete: true)
                .Index(t => t.RetailerId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Token = c.String(),
                        Language = c.String(nullable: false),
                        DisableNotifications = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CustomerJunctions", "RetailerId", "dbo.Retailers");
            DropForeignKey("dbo.RetailerLocations", "RetailerId", "dbo.Retailers");
            DropForeignKey("dbo.Retailers", "RetailerCategoryId", "dbo.RetailerCategories");
            DropForeignKey("dbo.Offers", "RetailerId", "dbo.Retailers");
            DropForeignKey("dbo.LoyaltyPoints", "CustomerJunctionId", "dbo.CustomerJunctions");
            DropForeignKey("dbo.CustomerJunctions", "LoyaltyCardId", "dbo.LoyaltyCards");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RetailerLocations", new[] { "RetailerId" });
            DropIndex("dbo.Offers", new[] { "RetailerId" });
            DropIndex("dbo.Retailers", new[] { "RetailerCategoryId" });
            DropIndex("dbo.LoyaltyPoints", new[] { "CustomerJunctionId" });
            DropIndex("dbo.CustomerJunctions", new[] { "RetailerId" });
            DropIndex("dbo.CustomerJunctions", new[] { "LoyaltyCardId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RetailerLocations");
            DropTable("dbo.RetailerCategories");
            DropTable("dbo.Offers");
            DropTable("dbo.Retailers");
            DropTable("dbo.LoyaltyPoints");
            DropTable("dbo.LoyaltyCards");
            DropTable("dbo.CustomerJunctions");
        }
    }
}
