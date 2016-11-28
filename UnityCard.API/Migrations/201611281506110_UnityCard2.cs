namespace UnityCard.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnityCard2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirebaseCloudMessagingRegistrationToken", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FirebaseCloudMessagingRegistrationToken");
        }
    }
}
