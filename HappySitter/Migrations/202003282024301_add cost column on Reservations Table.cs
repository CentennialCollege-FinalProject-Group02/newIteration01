namespace HappySitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcostcolumnonReservationsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "Cost", c => c.Double(nullable: false));
            AddColumn("dbo.Reservations", "PlatformFee", c => c.Double(nullable: false));
            AddColumn("dbo.Reservations", "TotalCost", c => c.Double(nullable: false));
            AddColumn("dbo.Reservations", "CostPerHour", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "CostPerHour");
            DropColumn("dbo.Reservations", "TotalCost");
            DropColumn("dbo.Reservations", "PlatformFee");
            DropColumn("dbo.Reservations", "Cost");
        }
    }
}
