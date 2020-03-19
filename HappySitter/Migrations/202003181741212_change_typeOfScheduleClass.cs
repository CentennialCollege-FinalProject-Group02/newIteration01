namespace HappySitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_typeOfScheduleClass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Schedules", "DateLastModified", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Schedules", "DateLastModified", c => c.Time(nullable: false, precision: 7));
        }
    }
}
