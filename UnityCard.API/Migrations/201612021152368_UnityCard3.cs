namespace UnityCard.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnityCard3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RetailerLocations", "Latitude");
            DropColumn("dbo.RetailerLocations", "Longitude");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RetailerLocations", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.RetailerLocations", "Latitude", c => c.Double(nullable: false));
        }
    }
}
