namespace HappySitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmorefieldonreservationtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "Hst", c => c.Double(nullable: false));
            AddColumn("dbo.AspNetUsers", "PlatformFeePercentage", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PlatformFeePercentage");
            DropColumn("dbo.Reservations", "Hst");
        }
    }
}
