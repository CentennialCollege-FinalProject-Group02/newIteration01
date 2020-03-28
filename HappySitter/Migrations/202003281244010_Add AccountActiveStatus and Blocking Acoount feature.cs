namespace HappySitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccountActiveStatusandBlockingAcoountfeature : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.AspNetUsers", "AccountActiveStatus", c => c.Int(nullable: false, defaultValue:2));
        }
        
        public override void Down()
        {
            //DropColumn("dbo.AspNetUsers", "AccountActiveStatus");
        }
    }
}
