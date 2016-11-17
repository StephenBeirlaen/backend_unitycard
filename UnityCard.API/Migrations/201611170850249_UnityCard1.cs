namespace UnityCard.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnityCard1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoyaltyCards", "UpdatedTimestamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.LoyaltyPoints", "UpdatedTimestamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.Retailers", "UpdatedTimestamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.Offers", "UpdatedTimestamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.RetailerCategories", "UpdatedTimestamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.RetailerLocations", "UpdatedTimestamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RetailerLocations", "UpdatedTimestamp");
            DropColumn("dbo.RetailerCategories", "UpdatedTimestamp");
            DropColumn("dbo.Offers", "UpdatedTimestamp");
            DropColumn("dbo.Retailers", "UpdatedTimestamp");
            DropColumn("dbo.LoyaltyPoints", "UpdatedTimestamp");
            DropColumn("dbo.LoyaltyCards", "UpdatedTimestamp");
        }
    }
}
