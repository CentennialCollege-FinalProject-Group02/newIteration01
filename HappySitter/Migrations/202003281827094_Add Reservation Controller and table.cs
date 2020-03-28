namespace HappySitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReservationControllerandtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(),
                        SitterId = c.String(),
                        ServiceDate = c.DateTime(nullable: false),
                        FromTime = c.Time(nullable: false, precision: 7),
                        ToTime = c.Time(nullable: false, precision: 7),
                        ReservationStatus = c.Int(nullable: false),
                        RegistrationDateTime = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        CancelDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reservations");
        }
    }
}
