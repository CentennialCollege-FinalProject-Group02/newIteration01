namespace HappySitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddusertablecostPerHour : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CostPerHour", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CostPerHour");
        }
    }
}
