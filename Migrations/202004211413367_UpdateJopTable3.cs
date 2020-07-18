namespace SFA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateJopTable3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jops", "Title", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Jops", "Watsapp", c => c.String());
            DropColumn("dbo.Jops", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jops", "Name", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Jops", "Watsapp");
            DropColumn("dbo.Jops", "Title");
        }
    }
}
