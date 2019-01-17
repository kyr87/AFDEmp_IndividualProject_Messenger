namespace AFDEmp_IndividualProject_Messenger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "Body", c => c.String(nullable: false, maxLength: 250));
            DropColumn("dbo.Messages", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Messages", "Body", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
