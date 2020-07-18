namespace SFA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateJopTable4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jops", "LastUpdated", c => c.DateTime());
            AlterColumn("dbo.Jops", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Jops", "CompanyName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Jops", "JopType", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jops", "JopType", c => c.String(maxLength: 255));
            AlterColumn("dbo.Jops", "CompanyName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Jops", "Title", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Jops", "LastUpdated");
        }
    }
}
