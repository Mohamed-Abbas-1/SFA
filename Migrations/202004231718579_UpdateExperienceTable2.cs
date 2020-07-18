namespace SFA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateExperienceTable2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Experiences", "FromMonthOrYear", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Experiences", "ToMonthOrYear", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Experiences", "TotalName", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Experiences", "TotalName", c => c.String(nullable: false));
            AlterColumn("dbo.Experiences", "ToMonthOrYear", c => c.String(nullable: false));
            AlterColumn("dbo.Experiences", "FromMonthOrYear", c => c.String(nullable: false));
        }
    }
}
