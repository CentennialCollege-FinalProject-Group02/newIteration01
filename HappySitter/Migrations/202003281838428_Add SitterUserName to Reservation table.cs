namespace HappySitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSitterUserNametoReservationtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "SitterUserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "SitterUserName");
        }
    }
}
