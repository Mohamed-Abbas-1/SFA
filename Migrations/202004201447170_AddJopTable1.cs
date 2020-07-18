namespace SFA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJopTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jops", "Details", c => c.String(nullable: false));
            AddColumn("dbo.Jops", "AnnouncedDate", c => c.DateTime());
            AlterColumn("dbo.Jops", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Jops", "CompanyName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Jops", "JopType", c => c.String(maxLength: 255));
            DropColumn("dbo.Jops", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jops", "MyProperty", c => c.Int(nullable: false));
            AlterColumn("dbo.Jops", "JopType", c => c.String());
            AlterColumn("dbo.Jops", "CompanyName", c => c.String());
            AlterColumn("dbo.Jops", "Name", c => c.String());
            DropColumn("dbo.Jops", "AnnouncedDate");
            DropColumn("dbo.Jops", "Details");
        }
    }
}
