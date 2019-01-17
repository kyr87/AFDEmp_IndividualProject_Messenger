namespace AFDEmp_IndividualProject_Messenger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRead : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Messages", "Read");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "Read", c => c.Boolean(nullable: false));
        }
    }
}
