namespace SFA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateExperienceTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Experiences", "From", c => c.Byte(nullable: false));
            AddColumn("dbo.Experiences", "FromMonthOrYear", c => c.String(nullable: false));
            AddColumn("dbo.Experiences", "To", c => c.Byte(nullable: false));
            AddColumn("dbo.Experiences", "ToMonthOrYear", c => c.String(nullable: false));
            AddColumn("dbo.Experiences", "TotalName", c => c.String(nullable: false));
            DropColumn("dbo.Experiences", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Experiences", "Name", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Experiences", "TotalName");
            DropColumn("dbo.Experiences", "ToMonthOrYear");
            DropColumn("dbo.Experiences", "To");
            DropColumn("dbo.Experiences", "FromMonthOrYear");
            DropColumn("dbo.Experiences", "From");
        }
    }
}
